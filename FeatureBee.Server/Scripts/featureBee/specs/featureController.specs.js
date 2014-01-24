/// <reference path="~/Scripts/featureBee/featureController.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />
/// <reference path="/Scripts/jasmine/jasmine-html.js" />

describe("featureController", function () {
    var featureController;
    var boardHub, editPanelHub, request, response;
    beforeEach(function () {
        boardHub = {
            server: {
                addNewItem : function () {}
            }
        };
        spyOn(boardHub.server, 'addNewItem');

        editPanelHub = {
            server: {
                editItem: function () { }
            }
        };
        spyOn(editPanelHub.server, 'editItem');

        request = {
            get: function(url) {
                return response;
            }
        };
        spyOn(request, 'get');

        featureController = new FeatureBeeController(boardHub, editPanelHub, request);
    });

    describe("when a new feature is created", function () {
        beforeEach(function() {
            featureController.add.post({});
        });

        it("should notify the server", function() {
            expect(boardHub.server.addNewItem).toHaveBeenCalled();
        });
    });

    describe("when a feature is edited", function () {
        var feature;

        beforeEach(function() {
            feature = featureController.edit.get("myfeature");
        });

        it("should have requested the feature from the server", function() {
            expect(request.get).toHaveBeenCalled();
        });

        describe("When saving changes to the feature", function() {
            beforeEach(function () {
                featureController.edit.post({});
            });

            it("should notify the server", function() {
                expect(editPanelHub.server.editItem).toHaveBeenCalled();
            });
        });
    });
});