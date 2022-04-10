<?php
header("Content-type: application/json; charset=utf-8");
require_once "config.php";
require 'classes/json_message.php';

if (!isset($_POST['email']) || empty($_POST['email'])){
    $message = new JsonMessage(400, "Campo email non inserito", false);
    echo json_encode($message);
    die();
}
if (!isset($_POST['nome']) || empty($_POST['nome'])){
    $message = new JsonMessage(400, "Campo nome non inserito", false);
    echo json_encode($message);
    die();
}
if (!isset($_POST['cognome']) || empty($_POST['cognome'])){
    $message = new JsonMessage(400, "Campo cognome non inserito", false);
    echo json_encode($message);
    die();
}
if (!isset($_POST['data']) || empty($_POST['data'])){
    $message = new JsonMessage(400, "Campo data non inserito", false);
    echo json_encode($message);
    die();
}
if (!isset($_POST['password']) || empty($_POST['password'])){
    $message = new JsonMessage(400, "Campo password non inserito", false);
    echo json_encode($message);
    die();
}

$email = $_POST['email'];
$nome = $_POST['nome'];
$cognome = $_POST['cognome'];
$dataDiNascita = $_POST['data'];
$password = $_POST['password'];

$sql= "INSERT INTO `utenti`(`Nome`, `Cognome`, `DataNascita`, `Email`, `Password`, `Admin`) VALUES ('$nome', '$cognome', '$dataDiNascita', '$email', md5('$password'), 0);";
try {
    if($result = mysqli_query($link, $sql)){
        $message = new JsonMessage(200, "Account creato", true);
        echo json_encode($message);
        die();
    } else {
        $message = new JsonMessage(406, "Impossibile creare l'account", true);
        echo json_encode($message);
        die();
    }
} catch (Throwable $th) {
    $message = new JsonMessage(500, mysqli_error($link), false);
        echo json_encode($message);
        die();
}
?>