// CES CMS Dashboard — site.js
// Quill WYSIWYG editor — brand-locked toolbar (no colors, fonts, headings, or images)

document.addEventListener('DOMContentLoaded', () => {

    if (typeof Quill !== 'undefined') {
        const toolbarOptions = [
            ['bold', 'italic', 'underline', 'strike'],
            [{ list: 'ordered' }, { list: 'bullet' }],
            ['link'],
            ['clean']
        ];

        // ── Create Modal Quill Editor ──────────────────────────────────────────
        const createContainer = document.getElementById('create-quill-editor');
        if (createContainer) {
            const quillCreate = new Quill('#create-quill-editor', {
                theme: 'snow',
                placeholder: 'Provide the detailed answer...',
                modules: { toolbar: toolbarOptions }
            });

            const createAnswerInput = document.getElementById('CreateAnswer');
            if (createAnswerInput) {
                quillCreate.on('text-change', () => {
                    createAnswerInput.value = quillCreate.root.innerHTML;
                });
            }

            const createFaqModal = document.getElementById('createFaqModal');
            if (createFaqModal) {
                createFaqModal.addEventListener('show.bs.modal', () => {
                    quillCreate.setContents([]);
                    if (createAnswerInput) createAnswerInput.value = '';
                });
                createFaqModal.addEventListener('shown.bs.modal', () => {
                    quillCreate.focus();
                });
            }
        }

        // ── Edit Modal Quill Editor ────────────────────────────────────────────
        const editContainer = document.getElementById('edit-quill-editor');
        if (editContainer) {
            const quillEdit = new Quill('#edit-quill-editor', {
                theme: 'snow',
                placeholder: 'Provide the detailed answer...',
                modules: { toolbar: toolbarOptions }
            });

            const editAnswerInput = document.getElementById('EditAnswer');
            if (editAnswerInput) {
                quillEdit.on('text-change', () => {
                    editAnswerInput.value = quillEdit.root.innerHTML;
                });
            }

            const editFaqModal = document.getElementById('editFaqModal');
            if (editFaqModal) {
                editFaqModal.addEventListener('show.bs.modal', (event) => {
                    const button = event.relatedTarget;
                    if (button) {
                        const id = button.getAttribute('data-id');
                        const question = button.getAttribute('data-question');
                        const answer = button.getAttribute('data-answer');
                        const displayOrder = button.getAttribute('data-displayorder');
                        const isPublished = button.getAttribute('data-ispublished') === 'true';

                        const form = editFaqModal.querySelector('form');
                        if (form) {
                            const idInput = form.querySelector('input[name="updatedFaq.Id"]');
                            if (idInput) idInput.value = id;

                            const questionInput = form.querySelector('input[name="updatedFaq.Question"]');
                            if (questionInput) questionInput.value = question;

                            const displayOrderInput = form.querySelector('input[name="updatedFaq.DisplayOrder"]');
                            if (displayOrderInput) displayOrderInput.value = displayOrder;

                            const isPublishedSwitch = form.querySelector('input[name="updatedFaq.IsPublished"]');
                            if (isPublishedSwitch) isPublishedSwitch.checked = isPublished;
                        }

                        quillEdit.root.innerHTML = answer || '';
                        if (editAnswerInput) editAnswerInput.value = answer || '';
                    }
                });
                editFaqModal.addEventListener('shown.bs.modal', () => {
                    quillEdit.focus();
                });
            }
        }
    }

    // ── Delete Modal ID Population ─────────────────────────────────────────
    const deleteFaqModal = document.getElementById('deleteFaqModal');
    if (deleteFaqModal) {
        deleteFaqModal.addEventListener('show.bs.modal', (event) => {
            const button = event.relatedTarget;
            if (button) {
                const id = button.getAttribute('data-id');
                const form = deleteFaqModal.querySelector('form');
                if (form) {
                    const idInput = form.querySelector('input[name="id"]');
                    if (idInput) idInput.value = id;
                }
            }
        });
    }
});
