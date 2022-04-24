<?php
require_once 'config.php';
require 'classes/json_message.php';
header("Content-type: application/json;charset=utf-8");

if ($_SERVER['REQUEST_METHOD'] != 'POST') {
    $message = new JsonMessage(405, "Method Not Allowed. Utilizzare metodo POST", false);
    echo json_encode($message);
    die();
}

if (!isset($_POST['type']) || empty($_POST['type'])){
    $message = new JsonMessage(400, "Tipo di aggiunta non specificato", false);
    echo json_encode($message);
    die();
}

$type = $_POST['type'];
switch ($type) {
    case 'edificio':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            $message = new JsonMessage(400, "Campo nome non inserito", false);
            echo json_encode($message);
            die();
        }
        if (!isset($_POST['indirizzo']) || empty($_POST['indirizzo'])){
            $message = new JsonMessage(400, "Campo indirizzo non inserito", false);
            echo json_encode($message);
            die();
        }
        
        $nome = $_POST['nome'];
        $indirizzo = $_POST['indirizzo'];
        
        $sql= "INSERT INTO `edifici`(`Nome`, `Indirizzo`) VALUES ('$nome', '$indirizzo')";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Edificio aggiunto", true);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere l'edificio", true);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
            $message = new JsonMessage(500, mysqli_error($link), false);
                echo json_encode($message);
                die();
        }
        break;
    case 'laboratorio':
        break;
    case 'pc':
        break;
    
    default:
        break;
}
?>