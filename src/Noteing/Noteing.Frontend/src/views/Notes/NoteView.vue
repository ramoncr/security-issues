<template>
  <div v-if="note" class="m-2">
    <div class="">
      <h3 for="exampleFormControlInput1" class="form-label">Title</h3>
      <input
        type="text"
        class="form-control"
        id="title"
        placeholder="My awesome note title"
        v-model="note.name"
      />
    </div>
    <h3 class="mt-2">Content</h3>
    <div class="editor" ref="editor"></div>
    <quill-editor theme="snow" v-model:content="note.content" contentType="html" />

    <div class="mt-2 float-end">
      <button class="btn btn-success" @click="save">Save</button>
      <button class="btn btn-primary ms-2" @click="share">Share</button>
      <button class="btn btn-danger ms-2">Delete</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, onUpdated, ref } from 'vue'
import api from '@/http-common'
import { useRoute } from 'vue-router'
import type { Note } from '@/models'
import { createShareLink } from '@/services/share.service';

const route = useRoute()

const note = ref<Note | undefined>()

const editor = ref<HTMLDivElement>()

onMounted(async () => {
  await loadContents()
})

onUpdated(async () => {
  const id = route.params.id

  if(id !== note.value?.id) {
    note.value = undefined;
    await loadContents()
  }
})

async function loadContents() {
  const id = route.params.id

  const response = await api.get<Note>('notes/' + id)
  note.value = response.data;
  note.value.croppedImages = [];
  note.value.shares = [];
}

async function save() {
  const id = route.params.id
  await api.put<Note>('notes/' + id, note.value);
  location.reload();
}


async function share() {
  if(!note.value) return;

  const link = createShareLink(note.value);
  navigator.clipboard.writeText(link);
}
</script>
