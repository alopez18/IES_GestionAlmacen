//SignalR script to update the chat page and send messages.
$(function () {
    if (!Modernizr.draganddrop) {
        alert("El navegador no soporta el sistema de arrastrado.")
    }

    // Reference the auto-generated proxy for the hub.
    var terminales = $.connection.terminalesHub;

    // Create a function that the hub can call back to display messages.
    terminales.client.activarTerminal = function (id, user) {
        var $terminal = $("#t" + id);
        $terminal.removeClass("disabled");
        $(".panel-footer > i", $terminal).removeClass("hide");
        $(".panel-footer > span", $terminal).text(user); //.removeClass("hide");
        console.log("activamos el terminal " + id + " con el usuario " + user);
        refreshCuerpoTabla();
    };


    terminales.client.desactivarTerminal = function (id) {
        var $terminal = $("#t" + id);
        $terminal.addClass("disabled");
        $(".panel-footer > i", $terminal).addClass("hide");
        $(".panel-footer > span", $terminal).text(""); //.removeClass("hide");
        console.log("desactivamos el terminal " + id);
        refreshCuerpoTabla();
    };

    terminales.client.setPCA2Terminal = function (idPCA, idTerminal, model) {
        var $terminal = $("#t" + idTerminal);
        $(".list-scrollable", $terminal).append(model);
    };



    //// Get the user name and store it to prepend to messages.
    //$('#displayname').val(prompt('Enter your name:', ''));
    //// Set initial focus to message input box.
    //$('#message').focus();

    // Start the connection.
    $.connection.hub.start().done(function () {
        //$('#sendmessage').click(function () {
        //    // Call the Send method on the hub.
        //    chat.server.send($('#displayname').val(), $('#message').val());
        //    // Clear text box and reset focus for next comment.
        //    $('#message').val('').focus();
        //});
    });

    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
        // Firefox does not fire cross-domain XHRs in the normal unload handler on tab close.
        // #2400
        $(window).on("beforeunload", function () {
            // If connection.stop() runs runs in beforeunload and fails, it will also fail
            // in unload unless connection.stop() runs after a timeout.
            window.setTimeout(function () {
                $.connection.hub.stop();
            }, 0);
        });
    }




    var elementDragging = null;
    $(".trDraggable").on("dragstart", function (e) {
        var $this = $(this);
        $this.css("opacity", "0.4");
        elementDragging = $this;

        $(".list-scrollable").hide();

        $(".listDroppable").addClass("dropping");
    });
    $(".trDraggable").on("dragend", function (e) {
        $(this).css("opacity", "1");
        $(".list-scrollable").show();
        $(".listDroppable").removeClass("dropping");
    });


    $(".listDroppable").on("dragenter", function (e) {
        var $this = $(this);
        $(".panel.listDroppable").addClass("notOver");
        $this.addClass("over");
    });


    $(".listDroppable").on("dragover", function (e) {
        if (e.preventDefault) {
            e.preventDefault(); // Necessary. Allows us to drop.
        }
        //e.dataTransfer.dropEffect = 'move';
        return false;
    });


    $(".listDroppable").on("dragleave", function (e) {
        $(".panel.listDroppable").removeClass("notOver");
        var $this = $(this);
        $this.removeClass("over");
    });

    $(".listDroppable").on("drop", function (e) {
        $(".panel.listDroppable").removeClass("notOver");
        var $this = $(this);
        $this.removeClass("over");

        if (e.stopPropagation) {
            e.stopPropagation(); // stops the browser from redirecting.
        }
        console.log(e);
        console.log($this);

        console.log(elementDragging);


        //alert("PCA con id " + elementDragging.data("id") + " arrastrado al terminal " + $this.data("id"));

        var datos = { pca: parseInt(elementDragging.data("id")), terminal: parseInt($this.data("id")) };


        $.ajax({
            method: "POST",
            url: "/backend/SetPCA2Terminal",
            data: datos,
            success: function (data) {
                if (data.Success) {
                    refreshCuerpoTabla();
                } else {
                    alert("Ha fallado!");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });

        return false;

    });

});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div/>').text(value).html();
    return encodedValue;
}

function refreshCuerpoTabla() {
    $.ajax({
        method: "POST",
        url: "/Almacen/GetCuerpoPCAs",
        success: function (data) {
            $("#tbPCAs").html(data);
        },
        error: function (err) {
            console.log(err);
            $("#tbPCAs").html(err);
        }
    });


}