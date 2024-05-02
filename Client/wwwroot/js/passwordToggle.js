// scripts.js
function togglePassword(inputId) {
    const inputElement = document.getElementById(inputId);
    const currentType = inputElement.type;

    // Cambiar el tipo de input basado en su estado actual
    if (currentType === 'password') {
        inputElement.type = 'text';
    } else {
        inputElement.type = 'password';
    }
}
