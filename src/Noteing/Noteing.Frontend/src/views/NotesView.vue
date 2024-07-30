<template>
  <main>
    <div class="row">
      <div class="col-3 pt-0">
        <div class="m-2">
          <h3 @click="router.push({ name: 'no-note'})">Notes</h3>
          <ol class="list-group list-group-numbered">
            <li v-for="note in notes" v-bind:key="note.id" class="list-group-item d-flex justify-content-between align-items-start" @click="open(note)">
              <div class="ms-2 me-auto">
                <div class="fw-bold">{{ note.name ?? "- No name -"}}</div>
                <span class="m-0 p-0" v-html="note.content?.substring(0, 50) ?? '- No content -'"></span>
              </div>
            </li>
          </ol>
        </div>
      </div>
      <div class="col-9">
        <RouterView />
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
import api from '@/http-common';
import type { Note } from '@/models';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

let notes = ref<Note[]>([])

const router = useRouter();

onMounted(async () => {
  await loadNotes();
});

async function loadNotes() {
  const { data } = await api.get<Note[]>('/notes');
  notes.value = data;
}

async function open(note: Note) {
  await router.push({
    name: 'note-details',
    params: {
      id: note.id
    }
  });

}


</script>

<style>
.btn-toggle::before {
  width: 1.25em;
  line-height: 0;
  content: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='rgba%280,0,0,.5%29' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M5 14l6-6-6-6'/%3e%3c/svg%3e");
  transition: transform 0.35s ease;
  transform-origin: 0.5em 50%;
}

[data-bs-theme='dark'] .btn-toggle::before {
  content: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='rgba%28255,255,255,.5%29' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M5 14l6-6-6-6'/%3e%3c/svg%3e");
}
</style>
