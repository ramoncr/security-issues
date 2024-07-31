import api from "@/http-common";
import type { Note } from "@/models";

function createShareLink(note: Note) {
    const host = window.location.host;
    const payload = {
        url: '/notes/'+ note.id + '/share',
        content: { noteId: note.id, sharer: note.owner },
    };

    const payloadString = JSON.stringify(payload);
    const payloadEncoded = btoa(payloadString);
    return `${host}/share/${payloadEncoded}`;
}

async function resolveSharePayload(encodedPayload: string) {
    const payloadString = atob(encodedPayload);
    const payload = JSON.parse(payloadString);
    await api.post(payload.url, payload.content);
}

export { createShareLink, resolveSharePayload }