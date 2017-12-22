<?php
    $result = 'Error 404';

    if(!empty($_POST['usernameCurrentPlayer']) ){
        include_once('lib/database_manager.php');
        $database = new databaseManager('api');
        $vScore = $database->SqlExecute("SELECT highscore FROM api.user WHERE username = '{$_POST['usernameCurrentPlayer']}' ;");
        $result = json_encode($vScore);
    }
           
    print_r($result);die;
?>


