$(function () {
    $.widget("as24.qr", {
        _create: function () {
            var self = this;
            $(self.element).find('[data-description="QR-Code Generation"]').find("p").hide();

            $(self.element).find('[data-generate="qr-code"]').click(function () {
                $(self.element).find('[data-description="QR-Code Generation"]').find("p").show();
                $(self.element).find('#qrcode_enable').empty();
                $(self.element).find('#qrcode_enable').qrcode(($(self.element).find('[name="urlQRCode"]').val()) + "?#" + ($(self.element).find('[name="name"]').val()) + "=true#");
                $(self.element).find('#qrcode_disable').empty();
                $(self.element).find('#qrcode_disable').qrcode(($(self.element).find('[name="urlQRCode"]').val()) + "?#" + ($(self.element).find('[name="name"]').val()) + "=false#");
            });
        }
    });
})