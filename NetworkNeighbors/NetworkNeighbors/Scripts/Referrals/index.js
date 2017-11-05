"use strict";

$(document).ready(function() {

    $("#facebookInvite").click(function() {
        SendFacebookInvite();
    });

    $("#googleInvite").click(function () {
        SendGoogleInvite();
    });

    $("#twitterInvite").click(function () {
        SendTwitterInvite();
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

function SendGoogleInvite() {
    alert("Coming soon!");
}

function SendTwitterInvite() {
    alert("Coming soon!");
}