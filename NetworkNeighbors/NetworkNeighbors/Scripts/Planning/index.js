"use strict";

$(document).ready(function () {

    var workflow = 0;

    // Show initial question
    $('.planning-question[data-question="1"]').show();

    $(".votingMethod").change(function () {

        console.log($(this).val());
        if ($(this).val() === "inPerson") {
            workflow = 1;
        } else {
            workflow = 2;
        }
        console.log(workflow);
    });

    $(".next").click(function() {

        var $planningQuestion = $(this).parents(".planning-question");
        var nextQuestion = $planningQuestion.data("question") + 1;
        $planningQuestion.hide();
        var $nextPlanningQuestion = $('.planning-question[data-workflow="' + workflow + '"][data-question="' + nextQuestion + '"');

        console.log($nextPlanningQuestion);

        if ($nextPlanningQuestion.length > 0) {
            $nextPlanningQuestion.show();
        } else {
            // Last question
            $("#finish").show();
        }

    });

    $("#lookupLocations").click(function () {

        $("#map").empty();

        var electionID = 2000; // TODO: Make this dynamic
        var address = $("#address").val();
        LookupPollingLocations(electionID, address);

    });

});

function LookupPollingLocations(electionID, address) {

    // Initialize Google API
    gapi.client.setApiKey(googleAPIKey);

    var request = gapi.client.request({
        path: "/civicinfo/v2/voterinfo",
        params: {
            electionId: electionID,
            address: address
        }
    });

    console.log(request);

    request.execute(RenderLocations);

}

/**
 * Render results in the DOM.
 * @param {Object} response Response object returned by the API.
 * @param {Object} rawResponse Raw response from the API.
 */
function RenderLocations(response, rawResponse) {

    console.log(response);

    var map = document.getElementById("map");

    if (!response || response.error) {
        map.appendChild(document.createTextNode(
            "Error while trying to fetch polling place"));
        return;
    }

    var normalizedAddress = response.normalizedInput.line1 + " " +
        response.normalizedInput.city + ", " +
        response.normalizedInput.state + " " +
        response.normalizedInput.zip;

    if (response.pollingLocations.length > 0) {

        var pollingLocation = response.pollingLocations[0].address;

        var pollingAddress = pollingLocation.locationName + ", " +
            pollingLocation.line1 + " " +
            pollingLocation.city + ", " +
            pollingLocation.state + " " +
            pollingLocation.zip;

        var normalizedMap = document.createElement("strong");
        normalizedMap.appendChild(document.createTextNode(
            "Polling place for " + normalizedAddress + ": "));
        map.appendChild(normalizedMap);
        map.appendChild(document.createTextNode(pollingAddress));

    } else {

        map.appendChild(document.createTextNode("Could not find polling place for " + normalizedAddress));
    }
}