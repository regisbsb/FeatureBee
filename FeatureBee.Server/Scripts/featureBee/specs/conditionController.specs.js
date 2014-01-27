/// <reference path="~/Scripts/featureBee/conditionController.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />
/// <reference path="/Scripts/jasmine/jasmine-html.js" />
/// <reference path="~/Scripts/jquery-1.9.1.js" />

describe("conditionController", function () {

    var allconditions = [];
    var conditions = [];
    var conditionController;

    beforeEach(function () {
        allconditions = [];
        conditions = [];
        allconditions.push({ type: "a" });
        allconditions.push({ type: "b" });
        conditionController = new ConditionController(allconditions);
    });

    describe("adding missing conditions", function() {

        describe("when no conditions are available", function() {
            beforeEach(function() {
                conditions = conditionController.addMissingConditions(conditions);
            });

            it("should add all the conditions to the array", function() {
                expect(conditions.length).toBe(2);
            });
        });

        describe("when not all conditions are available", function() {
            beforeEach(function() {
                conditions.push({ type: "a", values: "n" });
                conditions = conditionController.addMissingConditions(conditions);
            });

            it("should add the remaining conditions to the array", function() {
                expect(conditions.length).toBe(2);
            });

            it("should not change the values in the existing condition", function() {
                expect(conditions[0].values).toBe("n");
            });
        });

        describe("when more conditions are available", function() {
            beforeEach(function() {
                conditions.push({ type: "a", values: "n" });
                conditions.push({ type: "b", values: "n" });
                conditions.push({ type: "c", values: "n" });
                conditions = conditionController.addMissingConditions(conditions);
            });

            it("should not change the array", function() {
                expect(conditions.length).toBe(3);
                expect(conditions[0].values).toBe("n");
            });
        });
    });

    describe("trimming empty conditions", function () {
        describe("when all conditions are empty", function() {
            beforeEach(function()
            {
                conditions.push({ type: "a"});
                conditions.push({ type: "b" });
                conditions.push({ type: "c" });
                conditions = conditionController.trimEmptyConditions(conditions);
            });

            it("should return an empty array", function () {
                expect(conditions.length).toBe(0);
            });
        });

        describe("when no condition is empty", function() {
            beforeEach(function () {
                conditions.push({ type: "a", values: "n" });
                conditions.push({ type: "b", values: "n" });
                conditions.push({ type: "c", values: "n" });
                conditions = conditionController.trimEmptyConditions(conditions);
            });

            it("should return the complete array", function () {
                expect(conditions.length).toBe(3);
            });
        });

        describe("when some conditions are empty", function() {
            beforeEach(function () {
                conditions.push({ type: "a", values: "n" });
                conditions.push({ type: "b" });
                conditions.push({ type: "c", values: "n" });
                conditions = conditionController.trimEmptyConditions(conditions);
            });

            it("should remove the elements that are empty", function () {
                expect(conditions.length).toBe(2);
            });
        });
    });
})