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

});
