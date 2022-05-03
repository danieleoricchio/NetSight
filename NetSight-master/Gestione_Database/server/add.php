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
    die(json_encode(new JsonMessage(405, "Method Not Allowed. Utilizzare metodo POST", false, null)));
}

if (!isset($_POST['type']) || empty($_POST['type'])){
    die(json_encode(new JsonMessage(400, "Tipo di aggiunta non specificato", false, null)));
}

$type = $_POST['type'];
switch ($type) {
    case 'edificio':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            die(json_encode(new JsonMessage(400, "Campo nome non inserito", false, null)));
        }
        if (!isset($_POST['indirizzo']) || empty($_POST['indirizzo'])){
            die(json_encode(new JsonMessage(400, "Campo indirizzo non inserito", false, null)));
        }
        
        $nome = $_POST['nome'];
        $indirizzo = $_POST['indirizzo'];
        
        $sql= "INSERT INTO `edifici`(`Nome`, `Indirizzo`) VALUES ('$nome', '$indirizzo')";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Edificio aggiunto", true, null);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere l'edificio", true, null);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false, null);
                echo json_encode($message);
                die();
        }
        break;
    case 'laboratorio':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            die(json_encode(new JsonMessage(400, "Campo 'nome' non inserito", false, null)));
        }
        if (!isset($_POST['codedificio']) || empty($_POST['codedificio'])){
            die(json_encode(new JsonMessage(400, "Campo 'codedificio' non inserito", false, null)));
        }
        if (!isset($_POST['codadmin']) || empty($_POST['codadmin'])){
            die(json_encode(new JsonMessage(400, "Campo 'codadmin' non inserito", false, null)));
        }
        
        $nome = $_POST['nome'];
        $codedificio = $_POST['codedificio'];
        $codadmin = $_POST['codadmin'];
        
        $sql= "INSERT INTO `laboratori`(`Nome`, `CodEdificio`) VALUES ('$nome', '$codedificio')";
        $sql_select_codlab = "SELECT Cod FROM laboratori WHERE Nome = '$nome' AND CodEdificio = $codedificio";
        
        try {
            if(mysqli_query($link, $sql)){
                if ($result = mysqli_query($link, $sql_select_codlab)){
                    $codlab = mysqli_fetch_all($result)[0][0];
                    $sql_associazione = "INSERT INTO `gestione_laboratori`(`codLab`, `codAdmin`) VALUES ('$codlab','$codadmin')";
                    if ($result = mysqli_query($link, $sql_associazione)){
                        $message = new JsonMessage(200, "Laboratorio aggiunto", true, null);
                        echo json_encode($message);
                        die();
                    } else {
                        $message = new JsonMessage(406, "Impossibile aggiungere il collegamento tra l'admin e il laboratorio", false, null);
                        echo json_encode($message);
                        die();
                    }
                }
                else {
                    $message = new JsonMessage(406, "Impossibile selezionare il codice del laboratorio", false, null);
                    echo json_encode($message);
                    die();
                }
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere il laboratorio", false, null);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false, null);
                echo json_encode($message);
                die();
        }
        break;
    case 'pc':
        if (!isset($_POST['nome']) || empty($_POST['nome'])){
            die(json_encode(new JsonMessage(400, "Campo 'nome' non inserito", false, null)));
        }
        if (!isset($_POST['ip']) || empty($_POST['ip'])){
            die(json_encode(new JsonMessage(400, "Campo 'ip' non inserito", false, null)));
        }
        if (!isset($_POST['codlab']) || empty($_POST['codlab'])){
            die(json_encode(new JsonMessage(400, "Campo 'codlab' non inserito", false, null)));
        }
        
        $Nome = $_POST['nome'];
        $ip = $_POST['ip']; 
        $codlab = $_POST['codlab'];
        
        $sql= "INSERT INTO `pc`(`Nome`, `IndirizzoIp`, `Stato`, `CodLaboratorio`) VALUES ('$Nome', '$ip', '0', '$codlab')";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Pc aggiunto", true, null);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile aggiungere il pc", false, null);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false, null);
                echo json_encode($message);
                die();
        }
        break;
    default:
        break;
}
