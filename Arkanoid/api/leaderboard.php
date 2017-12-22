<?php
    $result = 'Error 403';

    if(isset($_GET['count'])) {
        if(is_numeric($_GET['count'])) {
            $count = intval($_GET['count']);
            if($count >= 0){
                include_once('lib/database_manager.php');
                $database = new databaseManager('api');
                $vScore = $database->SqlExecute("SELECT username, highscore FROM api.user ORDER BY highscore DESC LIMIT {$count};");
                $result = json_encode($vScore);
            }
        } 
    }
    
    print_r($result);die;
?>

