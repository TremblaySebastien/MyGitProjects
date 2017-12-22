<?php
    $result = 'Error 404';

    if(!empty($_POST['highscoreCurrentPlayer']) && !empty($_POST['usernameCurrentPlayer'])) {
        include_once('lib/database_manager.php');
        $database = new databaseManager('api');
        $vScore = $database->Sql("UPDATE api.user SET highscore = '{$_POST['highscoreCurrentPlayer']}' WHERE username = '{$_POST['usernameCurrentPlayer']}';");
        print ("changes made");
        $result = json_encode($vScore);
    }
          
    print_r($result);die;
?>



