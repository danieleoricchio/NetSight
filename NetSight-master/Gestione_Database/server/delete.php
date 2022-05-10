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
if ($_SERVER['REQUEST_METHOD'] != 'GET') {
    die(json_encode(new JsonMessage(405, "Method Not Allowed. Utilizzare metodo GET", false, null)));
}

if (!isset($_GET['type']) || empty($_GET['type'])){
    die(json_encode(new JsonMessage(400, "Tipo di eliminazione non specificato", false, null)));
}

$type = $_GET['type'];
switch ($type) {
    case 'laboratorio':
        if (!isset($_GET['cod']) || empty($_GET['cod'])){
            die(json_encode(new JsonMessage(400, "Campo 'cod' non inserito", false, null)));
        }
        
        $cod = $_GET['cod'];
        
        $sql= "DELETE FROM `laboratori` WHERE Cod = $cod;";
        
        try {
            if(mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Laboratorio eliminato", true, null);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile eliminare il laboratorio", false, null);
                echo json_encode($message);
                die();
            }
        } catch (Throwable $th) {
                $message = new JsonMessage(500, mysqli_error($link), false, null);
                echo json_encode($message);
                die();
        }
        break;
    case 'collegamento-lab':
        if (!isset($_GET['codlab']) || empty($_GET['codlab'])){
            die(json_encode(new JsonMessage(400, "Campo 'codlab' non inserito", false, null)));
        }
        if (!isset($_GET['codadmin']) || empty($_GET['codadmin'])){
            die(json_encode(new JsonMessage(400, "Campo 'codadmin' non inserito", false, null)));
        }
        $codlab = $_GET['codlab'];
        $codadmin = $_GET['codadmin'];
        $sql = "DELETE FROM gestione_laboratori WHERE codLab = $codlab AND codAdmin = $codadmin;";

        try {
            if (mysqli_query($link, $sql)){
                die(json_encode(new JsonMessage(200, "Hai lasciato il laboratorio", true, null)));
            } else {
                die(json_encode(new JsonMessage(406, "Impossibile lasciare il laboratorio", true, null)));
            }
        } catch (\Throwable $th) {
            die(json_encode(new JsonMessage(500, mysqli_error($link), false, null)));
        }
        mysqli_close($link);
        break;
    case 'pc':
        if (!isset($_GET['nome']) || empty($_GET['nome'])){
            die(json_encode(new JsonMessage(400, "Campo 'nome' non inserito", false, null)));
        }
        if (!isset($_GET['ip']) || empty($_GET['ip'])){
            die(json_encode(new JsonMessage(400, "Campo 'ip' non inserito", false, null)));
        }
        if (!isset($_GET['codlab']) || empty($_GET['codlab'])){
            die(json_encode(new JsonMessage(400, "Campo 'codlab' non inserito", false, null)));
        }
        
        $nome = $_GET['nome'];
        $ip = $_GET['ip']; 
        $codlab = $_GET['codlab'];
        
        $sql= "DELETE FROM `pc` WHERE Nome = '$nome' AND IndirizzoIp = '$ip' AND CodLaboratorio = $codlab";
        
        try {
            if($result = mysqli_query($link, $sql)){
                $message = new JsonMessage(200, "Pc eliminato", true, null);
                echo json_encode($message);
                die();
            } else {
                $message = new JsonMessage(406, "Impossibile eliminare il pc", false, null);
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
