$('documemt').ready(function () {
    var $categoryGroup = $(".ddlCategoryGroup");
    var $category = $(".ddlCategory");
    $.ajax({
        url: "/GetAllCategoryGroups",
        //data: { selectedValue: selectedValue },
        dataType: "json",
        type: "GET",
        error: function (err) {
            alert(" An error occurred: get category groups");
        },
        success: function (data) {
            var $option = $('<option>');
            if (data.length > 0) {
                var $defaultOption = $option.clone(true).attr("value", null).text("--Select Category Group--");
                $categoryGroup.append($defaultOption);
                $defaultOption = $option.clone(true).attr("value", null).text("--Any--");
                $category.append($defaultOption);

                $.each(data, function (i) {
                    var $currentOption = $option.clone(true)
                        .attr("value", data[i].CategoryGroupId).text(data[i].GroupName);
                    $categoryGroup.append($currentOption);
                });
            }
            else {
                $defaultOption = $option.clone(true).attr("value", null).text("--No Category Group--");
                $categoryGroup.append($defaultOption);
                $defaultOption = $option.clone(true).attr("value", null).text("--No Category--");
                $category.append($defaultOption);
            }
        }
    });

    $categoryGroup.on('change', function () {
        var $this = $(this);
        var groupId = $this.val();
        $.ajax({
            url: "/GetAllCategories",
            data: { groupId: groupId },
            dataType: "json",
            type: "GET",
            error: function () {
                alert(" An error occurred: get categories");
            },
            success: function (data) {
                var $option = $('<option>');
                if (data.length > 0) {
                    $category.find('option').remove();
                    $defaultOption = $option.clone(true).attr("value", 0).text("--Any--");
                    $category.append($defaultOption);

                    $.each(data, function (i) {
                        var $currentOption = $option.clone(true)
                            .attr("value", data[i].CategoryId).text(data[i].Name);
                        $category.append($currentOption);
                    });
                }
                else {
                    $defaultOption = $option.clone(true).attr("value", 0).text("--Any--");
                    $category.append($defaultOption);
                }

            }
        });
    });

});