document.addEventListener('DOMContentLoaded', function() {
    const fileInput = document.getElementById('imgInput');
    const labelImagen = document.getElementById('imgInputLabel');
    const imagen = document.getElementById('imgPreview');

    fileInput.addEventListener('change', function(event) {
        const file = event.target.files[0];
        if (file) {
            mostrarImagen(file);
        }
    });

    function mostrarImagen(file) {
        const reader = new FileReader();
        reader.onload = function(event) {
            imagen.src = event.target.result;
        };
        reader.readAsDataURL(file);
    }

    labelImagen.addEventListener('click', function() {
        fileInput.click();
    });
});