//Init
$(document).ready(function () {
    $.ajax({
        url: '/Project/ProjectExists',
        type: 'Get',
        contentType: 'application/json;',
        success: function (data) {
            var currentAction = '@ViewContext.RouteData.Values["action"].ToString()';
            var currentController = '@ViewContext.RouteData.Values["controller"].ToString()';

            console.log(currentAction + ' ' + currentController);
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
