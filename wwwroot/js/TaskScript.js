$(document).ready(function () {

    $('#clients').change(function () {
        var clientId = $('#clients').val();
        $.ajax
        ({
            //url: '@Url.Action("ClientProjects", "Application")',
            url: '/Application/ClientProjects',
            type: 'POST',
            data: { clientId: clientId },
            success: function (result, status, xhr) {
                console.log("Data received:", result);
                
                var options = '<option selected disabled value="">--Select Prjojects--</option>';
                if (result != null) {
                    $.each(result, function (index,item) {
                        console.log("Item:", item);
                        options += '<option value="' + item.projectID + '">' + item.projectName + '</option>';
                    });
                } else {
                    console.log("No data found");
                    options = '<option>No projects available</option>';
                }
                $('#projects').html(options);
            }
        })
    })

    $('#projects').change(function () {
        var projectId = $('#projects').val();
        $.ajax
            ({
                //url: '@Url.Action("ClientProjects", "Application")',
                url: '/Application/ProjectDataBases',
                type: 'POST',
                data: { projectId: projectId },
                success: function (result) {
                    console.log("Data received:", result);

                    var options = '<option disabled selected value=>--Select Project--</option>';;
                    if (result != null) {
                        $.each(result, function (index, item) {
                            console.log("Item:", item);
                            options += '<option value="' + item.dataBaseID + '">' + item.dataBaseName + '</option>';
                        });
                    } else {
                        console.log("No data found");
                        options = '<option>No projects available</option>';
                    }
                    $('#database').html(options);
                }
            })
    })
   
})