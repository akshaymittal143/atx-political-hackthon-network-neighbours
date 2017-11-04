"use strict";

$(document).ready(function() {

    $("#facebookInvite").click(function() {
        SendFacebookInvite();
    });

});

function SendFacebookInvite() {

    var link = "https://" + window.location.host + "/Referrals/Referral?ref=" + userID;
    
    FB.ui({
        method: "send",
        link: link
    }, function (response) {
        console.log(response);
    });
    
}