<?php

echo "Entered PHP file";

$servername = "localhost";
$username = "root";
$password = 1;
$dbname = "myDB";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 


$conn->close();
?>
