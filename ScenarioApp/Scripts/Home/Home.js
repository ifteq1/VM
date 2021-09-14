var HomeClass = /** @class */ (function () {
    function HomeClass() {
        this.initialise();
    }
    HomeClass.prototype.initialise = function () {
        var _this = this;
        toastr.options = { "positionClass": "toast-top-center", "newestOnTop": true };
        $("#fileUploadBtn").off("click").on("click", function () { return _this.fileUpload(); });
        $("#fileUploader").off("change").on("change", function (event) { return _this.fileUploadSelected(event); });
        $("#scenarioTable").bootstrapTable();
    };
    HomeClass.prototype.fileUpload = function () {
        $("#fileUploadNameTxt").val('');
        $("#fileUploader").click();
    };
    HomeClass.prototype.fileUploadSelected = function (event) {
        var input = event.target;
        var file = input.files[0];
        var fileNameLength = file.name.length;
        var fileNameExtension = file.name.substring((fileNameLength - 4), (fileNameLength));
        if (!(fileNameLength > 0 && fileNameExtension == ".xml")) {
            toastr["error"]("File type is not supported, please select a '.xml' file.");
        }
        else {
            $("#messagePanel").removeClass('hidden');
            var $form = $(event.currentTarget).parents('form');
            var model = new FormData();
            model.append("UploadedFile", file);
            $("#fileUploadNameTxt").val(file.name);
            $.ajax({
                dataType: 'json',
                url: $form.attr('action'),
                method: 'Post',
                data: model,
                cache: false,
                contentType: false,
                processData: false,
                success: function (result) {
                    $("#messagePanel").addClass('hidden');
                    if (result.code == 404) {
                        toastr["error"]("The file can not be found.  Please check the file and try again.", "Error");
                    }
                    else if (result.code == 409) {
                        toastr["error"]("The file format is incorrect.  The file does not contain relevant scenario data.", "Error");
                    }
                    else {
                        toastr["success"]("The data is successfully loaded", "Success");
                        $('#scenarioTable').bootstrapTable("destroy");
                        $("#scenarioTable").bootstrapTable({
                            data: result,
                            pagination: true,
                            pageSize: 10,
                            pageList: [10, 20, 30, 60, 100, 150, 200]
                        });
                    }
                }
            });
        }
    };
    return HomeClass;
}());
var home = new HomeClass();
//# sourceMappingURL=Home.js.map