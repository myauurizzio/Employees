
function ValidPhone() {
    //var re = /^(\+7|7|8)+[\d\(\)\ -]{4,14}\d$/; // - generic RU pattern
    var re = /^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$/; // - exact +7(XXX)XXX-XX-XX pattern
    var myPhone = document.getElementById('Phone').value;
    var valid = re.test(myPhone);

    if (valid) {
        document.getElementById('Phone').classList.remove('is-invalid');
          document.getElementById('Phone').classList.add('is-valid');
        }

    else {
        document.getElementById('Phone').classList.remove('is-valid');
        document.getElementById('Phone').classList.add('is-invalid');

    }

    return valid;
}

function ValidFirstName() {
    //var re = /^[а-яА-ЯёЁ\s\-]+$/; // -- Cyrillic unlimited 
    var re = /^[а-яА-ЯёЁ\s\-]{1,100}$/; // -- Cyrillic 
    var firstName = document.getElementById('FirstName').value;

    var valid = re.test(firstName);

    if (valid) {
        document.getElementById('FirstName').classList.remove('is-invalid');
        document.getElementById('FirstName').classList.add('is-valid');
    }
    else {
        document.getElementById('FirstName').classList.remove('is-valid');
        document.getElementById('FirstName').classList.add('is-invalid');
    }

    return valid;
}

function ValidGivenName() {
    var re = /^[а-яА-ЯёЁ\s\-]{1,100}$/;
    var givenName = document.getElementById('GivenName').value;

    var valid = re.test(givenName);

    if (valid) {
        document.getElementById('GivenName').classList.remove('is-invalid');
        document.getElementById('GivenName').classList.add('is-valid');
    }
    else {
        document.getElementById('GivenName').classList.remove('is-valid');
        document.getElementById('GivenName').classList.add('is-invalid');
    }


    return valid;
}

function ValidLastName() {
    var re = /^[а-яА-ЯёЁ\s\-]{1,100}$/;
    var lastName = document.getElementById('LastName').value;

    var valid = re.test(lastName);

    if (valid) {
        document.getElementById('LastName').classList.remove('is-invalid');
        document.getElementById('LastName').classList.add('is-valid');
    }
    else {
        document.getElementById('LastName').classList.remove('is-valid');
        document.getElementById('LastName').classList.add('is-invalid');
    }

    return valid;
}


function ValidateAll() {

    var v1 = ValidPhone();
    var v2 = ValidFirstName();
    var v3 = ValidGivenName();
    var v4 = ValidLastName();

    //return v;

    //return ValidPhone() && ValidFirstName() && ValidGivenName() && ValidLastName();

    return v1 && v2 && v3 && v4;
}


//window.onload = function () {
//    document.getElementById('Phone').onchange = ValidPhone;
//};

(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                //var v = ValidPhone();
                var v = ValidateAll();
                if (form.checkValidity() === false) {
                //if (ValidPhone() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                
                    form.classList.add('was-validated');
                
            }, false);
        });
    }, false);
})();