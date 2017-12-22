<?php

    function CreateToken($uid, $database) {
        $token = hash('ripemd160', rand(0, 100000));
        $expiration = time() + 1800; // 1800 for 30mins

        $database->Sql("INSERT INTO api.token (id, value, expiration, uid) VALUES (NULL, '{$token}', '{$expiration}', '{$uid}');");
        return $token;
    }

    if(!empty($_POST['username']) && !empty($_POST['password'])) {
        include_once('lib/database_manager.php');
        $database = new DatabaseManager('api');
        $encryptedPassword = md5($_POST['password']);

        $user = $database->SqlExecute("SELECT id FROM api.user WHERE username = '{$_POST['username']}' AND password = '{$encryptedPassword}';");

        if(!empty($user)) {
            $uid = $user[0]['id'];
                    
            $result = $database->SqlExecute("SELECT expiration, value FROM api.token WHERE uid='{$uid}'");
            if(!empty($result)) {
                $expiration = $result[0]['expiration'];

                if(time() <= $expiration) {
                    $token = $result[0]['value'];
                } else {
                    $database->Sql("DELETE FROM api.token WHERE uid='{$uid}'");
                    $token = CreateToken($uid, $database);
                }
            } else {
                //Generate Token
               $token = CreateToken($uid, $database);
            }
            
            print $token;
            die;
        } else {
            print 'Password or username not valid.';
            die;
        }

    }
    print 'Unautorize access';
    die;
?>