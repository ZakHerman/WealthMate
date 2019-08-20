<?php
include_once "Database.php";

$base_url = "https://api.worldtradingdata.com/api/v1/";
$symbol = "aapl";
$token = "";
$token_request_limit = 20;

function insertStockHistory($conn, $base_url, $symbol, $token) {
    $curl = curl_init();
    $time = strtotime("-1 year", time());
    $date = date("Y-m-d", $time);

    curl_setopt_array($curl, [
        CURLOPT_HTTPHEADER => array("Content-Type: application/json"),
        CURLOPT_SSL_VERIFYPEER => false,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_URL => $base_url . "history?symbol=" . $symbol . "&date_from=" . $date . "&api_token=" . $token,
        CURLOPT_USERAGENT => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36"
    ]);

    $resp = curl_exec($curl);
    curl_close($curl);

    $json = json_decode($resp, true);

    if (isset($json["history"])) {
        try {
            $stmt = $conn->prepare("INSERT INTO stock_history (stock, date, open, close, high, low, volume) VALUES (?, ?, ?, ?, ?, ?, ?)");

            foreach ($json["history"] as $day => $value) {
                $stmt->bindParam(1, $symbol, PDO::PARAM_STR, 10);
                $stmt->bindParam(2, $day, PDO::PARAM_STR, 10);
                $stmt->bindParam(3, $value["open"], PDO::PARAM_STR, 13);
                $stmt->bindParam(4, $value["close"], PDO::PARAM_STR, 13);
                $stmt->bindParam(5, $value["high"], PDO::PARAM_STR, 13);
                $stmt->bindParam(6, $value["low"], PDO::PARAM_STR, 13);
                $stmt->bindParam(7, $value["volume"], PDO::PARAM_INT);
                $stmt->execute();
            }

        } catch (PDOException $e) {
            echo "\nError: " . $e->getMessage();
        }
    } else {
        echo "\nNo History for: $symbol";
    }
}

function insertAllStockHistory($conn, $base_url, $token) {
    try {
        $stmt = $conn->prepare("SELECT symbol FROM stocks");
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

        foreach($result as $stock) {
            insertStockHistory($conn, $base_url, $stock["symbol"], $token);
        }
    } catch(PDOException $e) {
        echo "\nError: " . $e->getMessage();
    }
}

function insertCompaniesFromExchange($conn, $base_url, $exchange, $limit, $token) {
    $curl = curl_init();

    curl_setopt_array($curl, [
        CURLOPT_HTTPHEADER => array("Content-Type: application/json"),
        CURLOPT_SSL_VERIFYPEER => false,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_URL => $base_url . "stock_search?stock_exchange=" . $exchange . "&limit= " . $limit . "&api_token=" . $token,
        CURLOPT_USERAGENT => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36"
    ]);

    $resp = curl_exec($curl);
    $json = json_decode($resp, true);
    $pages = $json["total_pages"];

    try {
        $stmt = $conn->prepare("SELECT id FROM stock_exchange WHERE name = ?");
        $stmt->bindParam(1, $exchange);
        $stmt->execute();
        $result = $stmt->fetch(PDO::FETCH_ASSOC);

        if (isset($result["id"])) {
            $stmt = $conn->prepare("INSERT INTO company (name) VALUES (?)");

            for ($i = 1; $i <= $pages; $i++) {
                curl_setopt($curl, CURLOPT_URL, $base_url . "stock_search?stock_exchange=" . $exchange . "&limit= " . $limit . "&page=" . $i . "&api_token=" . $token);
                $resp = curl_exec($curl);
                $json = json_decode($resp, true);

                foreach ($json["data"] as $stock) {
                    if ($stock["name"] == "N/A") {
                        continue;
                    }

                    $stmt->bindParam(1, $stock["name"], PDO::PARAM_STR, 64);
                    $stmt->execute();
                }
            }
        } else {
            echo "\nInvalid exchange: $exchange";
        }
    } catch(PDOException $e) {
        echo "\nError: " . $e->getMessage();
    }

    curl_close($curl);
}

function insertSymbolsFromExchange($conn, $base_url, $exchange, $limit, $token) {
    $curl = curl_init();

    curl_setopt_array($curl, [
        CURLOPT_HTTPHEADER => array("Content-Type: application/json"),
        CURLOPT_SSL_VERIFYPEER => false,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_URL => $base_url . "stock_search?stock_exchange=" . $exchange . "&limit= " . $limit . "&api_token=" . $token,
        CURLOPT_USERAGENT => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36"
    ]);

    $resp = curl_exec($curl);
    $json = json_decode($resp, true);
    $pages = $json["total_pages"];

    try {
        $stmt = $conn->prepare("SELECT id FROM stock_exchange WHERE name = ?");
        $stmt->bindParam(1, $exchange);
        $stmt->execute();
        $result = $stmt->fetch(PDO::FETCH_ASSOC);

        if (isset($result["id"])) {
            $stmt = $conn->prepare("INSERT INTO stocks (symbol, company, price, exchange) VALUES (?, (SELECT id FROM company WHERE name = ?), ?, ?)");

            for ($i = 1; $i <= $pages; $i++) {
                curl_setopt($curl, CURLOPT_URL, $base_url . "stock_search?stock_exchange=" . $exchange . "&limit= " . $limit . "&page=" . $i . "&api_token=" . $token);
                $resp = curl_exec($curl);
                $json = json_decode($resp, true);

                foreach ($json["data"] as $stock) {
                    if ($stock["name"] == "N/A") {
                        continue;
                    }

                    $stmt->bindParam(1, $stock["symbol"], PDO::PARAM_STR, 10);
                    $stmt->bindParam(2, $stock["name"], PDO::PARAM_STR, 64);
                    $stmt->bindParam(3, $stock["price"], PDO::PARAM_STR, 13);
                    $stmt->bindParam(4, $result["id"], PDO::PARAM_INT);
                    $stmt->execute();
                }
            }
        } else {
            echo "\nInvalid exchange: $exchange";
        }
    } catch(PDOException $e) {
        echo "\nError: " . $e->getMessage();
    }

    curl_close($curl);
}

function updateStock($conn, $base_url, $symbols, $token) {
    $curl = curl_init();

    curl_setopt_array($curl, [
        CURLOPT_HTTPHEADER => array("Content-Type: application/json"),
        CURLOPT_SSL_VERIFYPEER => false,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_URL => $base_url . "stock?symbol=" . $symbols . "&api_token=" . $token,
        CURLOPT_USERAGENT => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36"
    ]);

    $resp = curl_exec($curl);
    $json = json_decode($resp, true);

    try {
        $stmt = $conn->prepare("UPDATE stocks SET price = ?, price_open = ?, day_high = ?, day_low = ?, 52_week_high = ?, 52_week_low = ?, shares = ?, volume = ? WHERE symbol = ?");

        foreach ($json["data"] as $stock) {
            $stmt->bindParam(1, $stock["price"], PDO::PARAM_STR, 13);
            $stmt->bindParam(2, $stock["price_open"], PDO::PARAM_STR, 64);
            $stmt->bindParam(3, $stock["day_high"], PDO::PARAM_STR, 13);
            $stmt->bindParam(4, $stock["day_low"], PDO::PARAM_STR, 13);
            $stmt->bindParam(5, $stock["52_week_high"], PDO::PARAM_STR, 13);
            $stmt->bindParam(6, $stock["52_week_low"], PDO::PARAM_STR, 13);
            $stmt->bindParam(7, $stock["shares"], PDO::PARAM_STR, 13);
            $stmt->bindParam(8, $stock["volume"], PDO::PARAM_INT);
            $stmt->bindParam(9, $stock["symbol"], PDO::PARAM_STR, 13);
            $stmt->execute();
        }
    } catch(PDOException $e) {
        echo "\nError: " . $e->getMessage();
    }
}

function updateStocksFromExchange($conn, $base_url, $limit, $exchange, $token) {
    try {
        $stmt = $conn->prepare("SELECT symbol FROM stocks WHERE exchange = (SELECT id FROM stock_exchange WHERE name = ?)");
        $stmt->bindParam(1, $exchange, PDO::PARAM_STR, 10);
        $stmt->execute();
        $result = $stmt->fetchAll();

        for ($i = 0; $i < count($result); $i += $limit) {
            $symbolsArr = array();

            for ($j = 0; $j < $limit; $j++) {
                if (isset($result[$i + $j])) {
                    array_push($symbolsArr, $result[$i + $j]["symbol"]);
                }
            }

            $symbols = implode(",", $symbolsArr);
            updateStock($conn, $base_url, $symbols, $token);
        }
    } catch(PDOException $e) {
        echo "\nError: " . $e->getMessage();
    }
}

//insertSymbolsFromExchange($conn, $base_url, "NZX", $token_request_limit, $token);
//insertStockHistory($conn, $base_url, "AIR.NZ", $token);
//updateStock($conn, $base_url, "ABA.NZ,AFC.NZ,AFT.NZ,AIA.NZ,AIR.NZ", $token);
//insertAllStockHistory($conn, $base_url, $token);
//updateStocksFromExchange($conn, $base_url, $token_request_limit, "NZX", $token);
//insertCompaniesFromExchange($conn, $base_url, "NZX", $token_request_limit, $token);