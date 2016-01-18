/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbar = 'Custom';
    config.contentsCss = '/Content/test-home.css'
    config.toolbar_Custom = [
    { name: 'styles', items: ['Styles', 'Format'] },
    { name: 'basicstyles', items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Blockquote'] },
        '/',
    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        { name: 'insert', items: ['Image'] },
        { name: 'tools', items: ['Maximize', '-', 'About'] }
    ];
};