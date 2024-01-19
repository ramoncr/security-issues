<template>
    <div class="my-note-list">
        <h2>My notes</h2>
        <ul>
            <li v-for="note in notes" :key="note.id">
            <p v-html="note.content"></p>
            <span>Notename: {{ note.name }}</span>
            </li>
        </ul>
    </div>
    
</template>

<script setup lang="ts">
import type { Note } from "@/models";
defineProps({
    notes: Array<Note>,
})
</script>