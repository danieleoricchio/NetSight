<?php
require_once 'config.php';
require 'classes/json_message.php';
header("Content-type: application/json; charset=utf-8");

$email = $_POST['email'];
$password = $_POST['password'];
if (!isset($email) || empty($email)){
    $message = new JsonMessage(400, "Email non inserita", false);
    echo json_encode($message);
    die();
}
if (!isset($password) || empty($password)){
    $message = new JsonMessage(400, "Password non inserita", false);
    echo json_encode($message);
    die();
}
$sql = "SELECT 'true' FROM utenti WHERE Email = '$email' AND Password = md5('$password');";
if($result = mysqli_query($link, $sql)){
    if (mysqli_num_rows($result) > 0){
        $message = new JsonMessage(200, "Login permesso", true);
        echo json_encode($message);
        die();
    } else {
        $message = new JsonMessage(401, "Email e/o password sbagliati", false);
        echo json_encode($message);
        die();
    }
}
else {
    $message = new JsonMessage(500, "Errore interno", false);
    echo json_encode($message);
    die();
}
?>