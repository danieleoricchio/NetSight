<?php
require 'classes/json_message.php';
header("Content-type: application/json;charset=utf-8");
define('DB_SERVER', 'localhost');
define('DB_USERNAME', 'root');
define('DB_PASSWORD', '');
define('DB_NAME', 'db_netsight');
$link = mysqli_connect(DB_SERVER, DB_USERNAME, DB_PASSWORD, DB_NAME);
if ($link === false) {
    die();
}
if ($_SERVER['REQUEST_METHOD'] != 'POST') {
    die(json_encode(new JsonMessage(405, "Method Not Allowed. Utilizzare metodo POST", false)));
}

if (!isset($_POST['type']) || empty($_POST['type'])){
    die(json_encode(new JsonMessage(400, "Tipo di aggiunta non specificato", false)));
}

$type = $_POST['type'];
switch ($type) {
    case 'edificio':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            die(json_encode(new JsonMessage(400, "Campo nome non inserito", false)));
        }
        if (!isset($_POST['indirizzo']) || empty($_POST['indirizzo'])){
            die(json_encode(new JsonMessage(400, "Campo indirizzo non inserito", false)));
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
        if (!isset($_POST['Nome']) || empty($_POST['Nome'])){
            die(json_encode(new JsonMessage(400, "Campo Nome non inserito", false)));
        }
        if (!isset($_POST['CodEdificio']) || empty($_POST['CodEdificio'])){
            die(json_encode(new JsonMessage(400, "Campo CodEdificio non inserito", false)));
        }
        
        $Nome = $_POST['Nome'];
        $CodEdificio = $_POST['CodEdificio'];
        
        $sql= "INSERT INTO `laboratori`(`Nome`, `CodEdificio`) VALUES ('$Nome', '$CodEdificio')";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Laboratorio non aggiunto", true);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere il laboratorio", true);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false);
                echo json_encode($message);
                die();
        }
        break;
    case 'pc':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            die(json_encode(new JsonMessage(400, "Campo Nome non inserito", false)));
        }
        if (!isset($_POST['ip']) || empty($_POST['ip'])){
            die(json_encode(new JsonMessage(400, "Campo ip non inserito", false)));
        }
        if (!isset($_POST['codLab']) || empty($_POST['codLab'])){
            die(json_encode(new JsonMessage(400, "Campo cod non inserito", false)));
        }
        
        $Nome = $_POST['nome'];
        $ip = $_POST['ip']; 
        $codLab = $_POST['codLab'];
        
        $sql= "INSERT INTO `pc`(`Nome`, `IndirizzoIp`, `Stato`, `CodLaboratorio`) VALUES ('$Nome', '$ip', '0', '$codLab')";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Pc non aggiunto", true);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere il pc", true);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false);
                echo json_encode($message);
                die();
        }
        break;
    default:
        break;
}
