function TogglePasswords() {
    var pwdInput = document.getElementById('pwd');
    var pwdConfirmInput = document.getElementById('pwdConfirm');

    if (pwdInput.type === 'password' && pwdConfirmInput.type === 'password') {
        pwdInput.type = 'text';
        pwdConfirmInput.type = 'text';
    } else {
        pwdInput.type = 'password';
        pwdConfirmInput.type = 'password';
    }
}
