"use strict";

//Custom Link2Web
$(document).ready(function () {

    //Custom Colors
    $("table tr:even").addClass("trcolor");
   
    
    //Show dialog if no project exists
    $.ajax({
        url: '/Project/ProjectExists',
        type: 'Get',
        contentType: 'application/json;',
        success: function (data) {
            if (!data) {
                BootstrapDialog.show({
                    title: 'Project',
                    message: 'Create a project first before going on.',
                    buttons: [{
                        label: 'Ok',
                        action: function (dialog) {
                            dialog.setClosable(false);
                            //dialog.setTitle('Ok');
                            window.location.href = '/Project/Create';
                        }
                    }]
                });
            }
        }
    });

    var keepSessionAlive = false;
    var keepSessionAliveUrl = null;

    function SetupSessionUpdater(actionUrl) {
        keepSessionAliveUrl = actionUrl;
        var container = $("#body");
        container.mousemove(function () { keepSessionAlive = true; });
        container.keydown(function () { keepSessionAlive = true; });
        CheckToKeepSessionAlive();
    }

    function CheckToKeepSessionAlive() {
        setTimeout("KeepSessionAlive()", 300000);
    }

    function KeepSessionAlive() {
        if (keepSessionAlive && keepSessionAliveUrl != null) {
            $.ajax({
                type: "POST",
                url: keepSessionAliveUrl,
                success: function () { keepSessionAlive = false; }
            });
        }
        CheckToKeepSessionAlive();
    }

    SetupSessionUpdater('/Home/KeepSessionAlive');

});
