<?php 
    $username = isset($_POST['username']) ? $_POST['username'] : null;
    $password = isset($_POST['password']) ? $_POST['password'] : null;
    $email = isset($_POST['email']) ? $_POST['email'] : null;

    if(!empty($username)) {
        if(!empty($password)) {
            if(!empty($email)) {
                $username = $_POST['username'];
                $password = md5($_POST['password']);
                $email = $_POST['email'];

                include_once 'lib/database_manager.php';
                $database = new DatabaseManager('api');
                $newUser = "INSERT INTO api.user (id, username, password, email, highscore)
                VALUES (NULL, '{$username}', '{$password}', '{$email}', '0')";
                $database->Sql($newUser);
                print 'New user succesfully added!!!';

            } else {
                print 'empty email field ... the user could not be added properly';
            }
        } else {
            print 'empty password field ... the user could not be added properly';
        }
    } else {
        print 'empty username field ... the user could not be added properly';
    }
?>

<html>
    <form action="register_web.php">
        <br>
        <button type="submit" value="Sauvegarder" name="sauvegarder">Retour a la page de creation</button>
    </form>
</html>
