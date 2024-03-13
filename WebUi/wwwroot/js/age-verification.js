

var sincity_modal_content,
sincity_modal_screen;

$(document).ready(function() {
	av_legality_check();
});


av_legality_check = function() {
	if ($.cookie('is_legal') == "yes") {
		// legal!
		// Do nothing?
	} else {
		av_showmodal();

		// Make sure the prompt stays in the middle.
		$(window).on('resize', av_positionPrompt);
	}
};

av_showmodal = function() {
	sincity_modal_screen = $('<div id="sincity_modal_screen"></div>');
	sincity_modal_content = $('<div id="sincity_modal_content" style="display:none"></div>');
	var sincity_modal_landing_container_wrapper = $('<div id="sincity_modal_landing_container_wrapper" class="landing_container_wrapper"></div>');
	var modal_regret_wrapper = $('<div id="modal_regret_wrapper" class="landing_container_wrapper" style="display:none;"></div>');

	// Question Content
	//var content_heading = $('<h2>Are you 18 or older?</h2>');
	var content_heading = $(''); //var content_heading = $('<div class="popUp_Logo"><img src="img/logo.png" alt="" /></div>');
	var content_buttons = $('<nav class="popup_button_group"><div class="terms_acceptCheck_box" style="display:none"><label><input type="checkbox" value="" type="checkbox" name="" /> </label></div><ul><li><a href="#nothing" class="av_btn av_go" rel="yes">ENTER</a></li><li><a href="https://www.google.com" class="av_btn2 av_no" rel="no">LEAVE</a></li></nav>');
	//var content_text = $('<p>You must verify that you are 18 years of age or older to enter this site.</p>');
	var content_text = $('<div class="sincity_popContent_block"><div class="model_imgBlock"><img src="img/model_img1.png" /></div><div class="sincity_popup_ageNotice"><p><strong class="adult_text">Disclaimer!</strong></p><p>SinCityExperience.com, including all webpages, links and images , displays sexually explicit material. Only consenting adults are authorized beyond this page.</p><p>  If you are a minor (under the age of 18 years or 21 years where 18 isn\'t the legal age of majority), if sexually explicit material offends you or if it\'s illegal to view such material in your community, you MUST leave this site by clicking "LEAVE" below.</p><p>By entering SinCityExperience.com you agree that you are an adult in your community and are at least 18 years old (21 in some countries), you agree with terms and conditions, you agree that sexually explicit material is not deemed to be obscene or illegal in your community, you accept full responsibility for your own actions as well as you agree to the use of cookies.</p></div></div>');
	// Regret Content
	var regret_heading = $('<h2>We\'re Sorry!</h2>');
	var regret_buttons = $('<nav><small>I hit the wrong button!</small> <ul><li><a href="#nothing" class="av_btn av_go" rel="yes">I\'M OLD ENOUGH!</a></li></ul></nav>');
	var regret_text = $('<p>You must be 18 years of age or older to enter this site.</p>');
	
	
	

	sincity_modal_landing_container_wrapper.append(content_heading, content_text, content_buttons);
	modal_regret_wrapper.append(regret_heading, regret_buttons, regret_text);
	sincity_modal_content.append(sincity_modal_landing_container_wrapper, modal_regret_wrapper);

	// Append the prompt to the end of the document
	$('body').append(sincity_modal_screen, sincity_modal_content);

	// Center the box
	av_positionPrompt();

	sincity_modal_content.find('a.av_btn').on('click', av_setCookie);
};

av_setCookie = function(e) {
	e.preventDefault();

	var is_legal = $(e.currentTarget).attr('rel');

	$.cookie('is_legal', is_legal, {
		expires: 30,
		path: '/'
	});

	if (is_legal == "yes") {
		av_closeModal();
		$(window).off('resize');
	} else {
		av_showRegret();
	}
};

av_closeModal = function() {
	sincity_modal_content.fadeOut();
	sincity_modal_screen.fadeOut();
};

av_showRegret = function() {
	sincity_modal_screen.addClass('nope');
	sincity_modal_content.find('#sincity_modal_landing_container_wrapper').hide();
	sincity_modal_content.find('#modal_regret_wrapper').show();
};

av_positionPrompt = function () {
	const top = (window.innerHeight - $('#sincity_modal_content').outerHeight()) / 2;
	const left = ($(window).outerWidth() - $('#sincity_modal_content').outerWidth()) / 2;
	sincity_modal_content.css({
		'top': top,
		'left': left
	});

	if (sincity_modal_content.is(':hidden') && ($.cookie('is_legal') != "yes")) {
		sincity_modal_content.fadeIn('slow')
	}
};
