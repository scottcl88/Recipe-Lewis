
$(document).on('focusin', '#tags:not(".readonly") > input[type=text]', function () {
    var tagValue = $("#hiddenTagInput").val();
    var availableTags = tagValue.split(',');
    $('#tags:not(".readonly") > input[type=text]').autocomplete({
        source: availableTags,
        select: function (event, ui) {
            //small hack workaround to trigger the change which is handled by code to actually add the tag to the model, then refocus it
            $("#selectFileButton").focus();
            setTimeout(() => {
                $("#tagInput").focus();
            }, 300);
            return false;
        }
    });
});
$(document).on('keyup', '#tags:not(".readonly") > input[type=text]', function (e) {
    if (/(188|13|32)/.test(e.which)) {
        //small hack workaround to trigger the change which is handled by code to actually add the tag to the model, then refocus it
        $("#selectFileButton").focus();
        setTimeout(() => {
            $("#tagInput").focus();
        }, 100);
    }
});
$(document).on('keyup', '.ingredientInput', function (e) {
    if (event.keyCode == 13) {
        var sectionId = $(e.target).data("section-id");
        $(".ingredientAdd[data-section-id='" + sectionId + "']").focus();
        setTimeout(() => {
            $(".ingredientAdd[data-section-id='" + sectionId + "']").click();
            $(".ingredientInput[data-section-id='" + sectionId + "']").focus();
        }, 100);
    }
});
$(document).on('keyup', '#stepInput', function (e) {
    if (event.keyCode == 13) {
        $("#stepAdd").focus();
        setTimeout(() => {
            $("#stepAdd").click();
            $("#stepInput").focus();
        }, 100);
    }
});
$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
});

$(document).on("click", ".images img", (e) => {
    $("#full-image").attr("src", $(e.target).attr("src"));
    $('#image-viewer').show();
});
$(document).on("click", "#image-viewer .close", () => {
    $('#image-viewer').hide();
});
$(document).on("click", "#selectFileButton", () => {
    $('#files').click();
});