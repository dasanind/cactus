<?php
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

// sql to create table
$sql = "CREATE TABLE User (
id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
username VARCHAR(30) NOT NULL,
score INT(11) NOT NULL
)";

if ($conn->query($sql) === TRUE) {
  echo "Table Score created successfully";
} else {
  echo "Error creating table: " . $conn->error;
}
