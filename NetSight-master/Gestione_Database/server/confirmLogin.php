<?php
require_once 'config.php';
require 'classes/json_message.php';
require 'classes/utente.php';
header("Content-type: application/json; charset=utf-8");
if ($_SERVER['REQUEST_METHOD'] != 'POST') {
    $message = new JsonMessage(405, "Method Not Allowed. Utilizzare metodo POST", false, null);
    echo json_encode($message);
    die();
}
if (!isset($_POST['email']) || empty($_POST['email'])){
    $message = new JsonMessage(400, "Email non inserita", false, null);
    echo json_encode($message);
    die();
}
if (!isset($_POST['password']) || empty($_POST['password'])){
    $message = new JsonMessage(400, "Password non inserita", false, null);
    echo json_encode($message);
    die();
}
if (!isset($_POST['account_type']) || empty($_POST['account_type'])){
    $account_type = "user";
} else {
    $account_type = $_POST['account_type'];
}
$isAdmin = $account_type == "admin";
$email = $_POST['email'];
$password = $_POST['password'];
$sql = "SELECT * FROM " . ($isAdmin ? "admin" : "utenti") ." WHERE Email = '$email' AND Password = md5('$password');";
if($result = mysqli_query($link, $sql)){
    if (mysqli_num_rows($result) > 0){
        $all = mysqli_fetch_all($result);
        if ($isAdmin){
            $message = new JsonMessage(202, "Login permesso (Admin)", true, new utente(intval($all[0][0]), $all[0][1], $all[0][2], $all[0][3], $all[0][4]));
        }
        else {
            $message = new JsonMessage(200, "Login permesso (Utente)", true, new utente(intval($all[0][0]), $all[0][1], $all[0][2], $all[0][3], $all[0][4]));
        }
        echo json_encode($message);
        die();
    } else {
        $message = new JsonMessage(401, "Email e/o password sbagliati", false, null);
        echo json_encode($message);
        die();
    }
}
else {
    $message = new JsonMessage(500, "Errore interno", false, null);
    echo json_encode($message);
    die();
}
?>