// CES CMS Dashboard — site.js
// Quill WYSIWYG editor — brand-locked toolbar (no colors, fonts, headings, or images)

document.addEventListener('DOMContentLoaded', () => {

    // ── Quill Editor Initialization ──────────────────────────────────────────
    const editorContainer = document.getElementById('faq-editor-container');

    if (editorContainer && typeof Quill !== 'undefined') {

        const quill = new Quill('#faq-editor-container', {
            theme: 'snow',
            placeholder: 'Provide the detailed answer...',
            modules: {
                toolbar: [
                    // Allowed: Bold, Italic, Underline, Strike
                    ['bold', 'italic', 'underline', 'strike'],
                    // Allowed: Ordered list, Bullet list
                    [{ list: 'ordered' }, { list: 'bullet' }],
                    // Allowed: Link
                    ['link'],
                    // Clean formatting
                    ['clean']
                    // PROHIBITED: color, background, font, size, header, image
                ]
            }
        });

        // Sync Quill HTML content to the hidden input on every change
        // so the form captures the rich text on submission.
        const answerInput = document.getElementById('Answer');
        if (answerInput) {
            quill.on('text-change', () => {
                answerInput.value = quill.root.innerHTML;
            });
        }

        // Sync when the FAQ modal opens so existing data could be pre-loaded
        const faqModal = document.getElementById('faqModal');
        if (faqModal) {
            faqModal.addEventListener('shown.bs.modal', () => {
                quill.focus();
            });
            // Reset editor when modal is dismissed
            faqModal.addEventListener('hidden.bs.modal', () => {
                quill.setContents([]);
                if (answerInput) answerInput.value = '';
            });
        }
    }

});
