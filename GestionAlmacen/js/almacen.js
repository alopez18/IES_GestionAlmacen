var dragging = {};
dragging.trDragging = null;
dragging.listDragging = null;
dragging.terminalDraggingId = null;


$(function () {
    if (!Modernizr.draganddrop) {
        alert("El navegador no soporta el sistema de arrastrado.");
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

    terminales.client.refreshPCAinTerminales = function (id) {
        GetPCAenTerminal(id);
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


    terminales.client.movePCA = function (idPCA, idTerminalOld, idTerminalNew, model) {
        var $pcaOld = $("#t" + idTerminalOld + " .pcaterminal" + idPCA);
        $pcaOld.remove();
        var $terminalNew = $("#t" + idTerminalNew);
        var $list = $(".list-scrollable", $terminalNew);
        $list.append(model);
    }



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





    dragging.$trDraggable = $(".trDraggable");
    dragging.$listDraggable = $(".listDraggable");
    dragging.$listDroppable = $(".listDroppable");
    dragging.$list_scrollable = $(".list-scrollable");
    dragging.$panelDroppable = $(".panel.listDroppable");


    dragging.$trDraggable.on("dragstart", trsDragstartEvent);
    dragging.$trDraggable.on("dragend", function (e) {
        $(this).css("opacity", "1");
        dragging.$list_scrollable.show();
        dragging.$listDroppable.removeClass("dropping");
    });


    dragging.$listDraggable.on("dragstart", function (e) {
        var $this = $(this);
        //$this.css("opacity", "0.4");
        dragging.listDragging = $this;

        var terminal = $this.parents(".listDroppable");
        if (terminal.length > 0) {
            dragging.terminalDraggingId = $(terminal[0]).data("id");
        }
        $this.parents(".list-scrollable").addClass("dragging");
        $(".list-scrollable:not(.dragging)").hide();
        dragging.$listDroppable.addClass("dropping");
    });
    dragging.$listDraggable.on("dragend", function (e) {
        var $this = $(this);
        $this.parents(".list-scrollable").removeClass("dragging");
        $this.css("opacity", "1");
        dragging.$list_scrollable.show();
        dragging.$listDroppable.removeClass("dropping");
    });



    dragging.$listDroppable.on("dragenter", function (e) {
        var $this = $(this);
        dragging.$panelDroppable.addClass("notOver");
        $this.addClass("over");
    });


    dragging.$listDroppable.on("dragover", function (e) {
        if (e.preventDefault) {
            e.preventDefault(); // Necessary. Allows us to drop.
        }
        //e.dataTransfer.dropEffect = 'move';
        return false;
    });


    dragging.$listDroppable.on("dragleave", function (e) {
        dragging.$panelDroppable.removeClass("notOver");
        var $this = $(this);
        $this.removeClass("over");
    });

    dragging.$listDroppable.on("drop", function (e) {
        dragging.$panelDroppable.removeClass("notOver");
        var $this = $(this);
        $this.removeClass("over");

        if (e.stopPropagation) {
            e.stopPropagation(); // stops the browser from redirecting.
        }
        //console.log(e);
        //console.log($this);
        //console.log(dragging.trDragging);
        //alert("PCA con id " + dragging.trDragging.data("id") + " arrastrado al terminal " + $this.data("id"));


        if (dragging.trDragging != null) {
            var datos = { pca: parseInt(dragging.trDragging.data("id")), terminal: parseInt($this.data("id")) };
            $.ajax({
                method: "POST",
                url: "/backend/SetPCA2Terminal",
                data: datos,
                success: function (data) {
                    if (data.Success) {
                        dragging.trDragging = null;
                        refreshCuerpoTabla();
                    } else {
                        alert("Ha fallado!");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } else if (dragging.listDragging != null) {
            var datos = { pca: parseInt(dragging.listDragging.data("id")), terminalOld: dragging.terminalDraggingId, terminalNew: parseInt($this.data("id")) };
            $.ajax({
                method: "POST",
                url: "/backend/MovePCA2Terminal",
                data: datos,
                success: function (data) {
                    if (data.Success) {
                        //$("#t" + dragging.terminalDraggingId + " .pcaterminal" + dragging.listDragging.data("id")).remove();
                        dragging.listDragging = null;
                        dragging.terminalDraggingId = null;
                        refreshCuerpoTabla();
                    } else {
                        alert("Ha fallado!");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } else {


        }



        return false;

    });
});


function trsDragstartEvent(e) {
    var $this = $(this);
    $this.css("opacity", "0.4");
    dragging.trDragging = $this;
    dragging.$list_scrollable.hide();
    dragging.$listDroppable.addClass("dropping");
}



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


function GetPCAenTerminal(id) {
    $.ajax({
        method: "POST",
        url: "/Almacen/GetPCAenTerminal/" + id,
        success: function (data) {
            $(".pcaterminal" + id).replaceWith(data);
        },
        error: function (err) {
            console.log(err);
            $(".pcaterminal" + id).html(err);
        }
    });
}