var databaseHtml = {};
var sizeDB = 0;

window.indexedDB = window.indexedDB || window.msIndexedDB
    || window.webkitIndexedDB || window.mozIndexedDB;

if ('webkitIndexedDB' in window) {
    window.IDBTransaction = window.webkitIDBTransaction;
    window.IDBKeyRange = window.webkitIDBKeyRange;
}

databaseHtml.indexedDB = {};

databaseHtml.indexedDB.db = null;

databaseHtml.indexedDB.onerror = function (e) {
    console.log(e);
};

databaseHtml.indexedDB.open = function () {
    var versao = 2;

    var request = indexedDB.open("Html5", versao);

    request.onupgradeneeded = function (e) {
        var db = e.target.result;

        e.target.transaction.onerror = databaseHtml.indexedDB.onerror;

        if (db.objectStoreNames.contains("Aluno")) {
            db.deleteObjectStore("Aluno");
        }

        var repositorio = db.createObjectStore("Aluno"
            , { keyPath: "timeStamp" });

    };

    request.onsuccess = function (e) {
        databaseHtml.indexedDB.db = e.target.result;
        databaseHtml.indexedDB.getAlunos();
    }

    request.onerror = databaseHtml.indexedDB.onerror;
};

databaseHtml.indexedDB.getAlunos = function () {
    var table = $("#dados");
    table.empty();

    var db = databaseHtml.indexedDB.db;
    var trans = db.transaction("Aluno", "readwrite");
    var repositorio = trans.objectStore("Aluno");

    var keyRange = IDBKeyRange.lowerBound(0);
    var cursosRequest = repositorio.openCursor(keyRange);

    cursosRequest.onsuccess = function (e) {
        var result = e.target.result;


        if (!!result == false) {
            return;
        }

        criarLinha(result.value);

        sizeDB += JSON.stringify(result.value).length;

        result.continue();
    };

    cursosRequest.onfinish = function (e) {
        alert(sizeDB);
    }

    cursosRequest.onerror = databaseHtml.indexedDB.onerror;
};

databaseHtml.indexedDB.addAluno = function (aluno) {
    var db = databaseHtml.indexedDB.db;

    var trans = db.transaction("Aluno", "readwrite");

    var repositorio = trans.objectStore("Aluno");


    aluno.End = {
        Log: "Rua A",
        Numero: 80,
        cidade: "Salvador",
        estado: "BA"
    };
    if (navigator.onLine) {
        $.post("bla", aluno);
    } else {
        var request = repositorio.add(aluno);
    }
    

    request.onsuccess = function (e) {
        criarLinha(aluno);
    }
    request.onerror = function (e) {
        console.log("Erro: " + e);
    }
};

databaseHtml.indexedDB.delete = function (id) {
    var db = databaseHtml.indexedDB.db;
    var trans = db.transaction("Aluno", "readwrite");
    var repo = trans.objectStore("Aluno");

    var request = repo.delete(id);

    request.onsuccess = function (e) {
        excluirLinha(id);
    }
};

databaseHtml.indexedDB.update = function (aluno) {

    var db = databaseHtml.indexedDB.db;
    var trans = db.transaction("Aluno", "readwrite");
    var repo = trans.objectStore("Aluno");

    var request = repo.put(aluno);

    request.onsuccess = function (e) {
        alterarLinha(aluno);
    }
}

function alterarLinha(aluno) {
    $("#" + aluno.timeStamp + " td:eq(0)").html(aluno.Nome);
    $("#" + aluno.timeStamp + " td:eq(1)").html(aluno.Idade);
    $("#" + aluno.timeStamp + " td:eq(2)").html(aluno.Email);
}

function excluirLinha(id) {
    var tr = $("#" + id).closest('tr');
    tr.fadeOut(400, function () {
        tr.remove();
    });
}

function criarLinha(aluno) {
    debugger;
    var table = $("#dados");
    var colunaNome = "<td>" + aluno.Nome + "</td>";
    var colunaIdade = "<td>" + aluno.Idade + "</td>";
    var colunaEmail = "<td>" + aluno.Email + "</td>";
    var colunaExcluir = "<td><a href='#' class='del' id='"
        + aluno.timeStamp + "'>Excluir</a></td>";
    var colunaSelecionar = "<td><a href='#' class='sel' id='"
        + aluno.timeStamp + "'>Selecionar</a></td>";

    table.append("<tr id='" + aluno.timeStamp + "'>"
        + colunaNome + colunaIdade + colunaEmail
        + colunaExcluir + colunaSelecionar + "</tr>");

    $(".del").click(function () {
        var id = $(this).attr("id");
        databaseHtml.indexedDB.delete(parseInt(id));
    });

    $(".sel").click(function () {
        var id = $(this).attr("id");

        $("#txtId").val(id);
        $("#txtNome").val($("#" + id + " td:eq(0)").html());
        $("#txtIdade").val($("#" + id + " td:eq(1)").html());
        $("#txtEmail").val($("#" + id + " td:eq(2)").html());

    });

}

function addAluno() {
    var id = $("#txtId").val();
    var objAluno;

    if (id.trim().length == 0) {
        objAluno = new Aluno($("#txtNome").val(),
            $("#txtIdade").val(),
            $("#txtEmail").val(),
            new Date().getTime());

        databaseHtml.indexedDB.addAluno(objAluno);
    }
    else {
        objAluno = new Aluno($("#txtNome").val(),
            $("#txtIdade").val(),
            $("#txtEmail").val(),
            parseInt($("#txtId").val()));
        databaseHtml.indexedDB.update(objAluno);
    }
}

$(document).ready(function () {
    databaseHtml.indexedDB.open();
});




