<?php
//save datas from client
$email = $_POST['mail'];
$nome = $_POST['nome'];
$cognome = $_POST['cognome'];
$dataDiNascita = $_POST['data'];

//controllo che non manchi nessun campo
if(!empty($email) && !empty($nome) && !empty($cognome) && !empty($dataDiNascita)){
    //controlla
    InsertSQLWithRandomPassword($link,$nome,$cognome,$email,$dataDiNascita);
    if(mysqli_num_rows($result)==1){
        //setto la sessione
        $_SESSION['user_id'] = $nome;
        echo "Successful";
        //SendPacket($userIP, $socket);
    }else{
        header('Location: index.html');
    }
}
else{
    echo "Error, cannot execute the insert to database SQL...";
}

//funzione che crea una password a caso
function RandomPassword($length){
    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    $charactersLength = strlen($characters);
    $randomString = '';
    for ($i = 0; $i < $length; $i++) {
        $randomString .= $characters[rand(0, $charactersLength - 1)];
    }
    return $randomString;
}

//funzione che crea l'sql di INSERT
function InsertSQLWithRandomPassword($link, $nome, $cognome, $email, $dataDiNascita){
    //gli passo la lunghezza della password
    $password = RandomPassword(10);
    $sql= "INSERT INTO `users`(`nome`, `cognome`, `dataNascita`, `email`, `password`) VALUES (".$nome.",".$cognome.",".$dataDiNascita.",".$email.";".md5($password).")";
    mysqli_query($link,$sql);
}
?>