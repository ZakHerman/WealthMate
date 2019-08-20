<?php

$servername = "localhost";
$username = "wealthmate";
$password = "wealthmate";
$db = "wealthmate";

/*$servername = "wealthmate.mysql.database.azure.com";
$username = "wealthmateadmin@wealthmate";
$password = "WealthMate123";
$db = "wealthmate";*/
//WealthMate123

try {
    $conn = new PDO("mysql:host=$servername;dbname=$db", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    echo "Connected successfully";
} catch(PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}