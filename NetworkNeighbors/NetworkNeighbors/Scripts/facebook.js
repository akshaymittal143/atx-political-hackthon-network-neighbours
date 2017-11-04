"use strict";

window.fbAsyncInit = function() {
      FB.init({
          appId            : facebookAppID, /* Must be passed in by any page that loads this script. Hint: Use Razor variable. */
          autoLogAppEvents : true,
          xfbml            : true,
          version          : 'v2.10'
      });
      FB.AppEvents.logPageView();
  };

(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));