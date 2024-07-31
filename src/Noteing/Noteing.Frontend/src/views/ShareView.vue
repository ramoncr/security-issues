<template>
  <div class="m-2">
    <h3 v-if="loading">Accepting share request, we are providing you access.</h3>
    <div v-if="!loading">
      <h3>Share request accepted</h3>
      <p>You can now view the note.</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { resolveSharePayload } from '@/services/share.service'
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const loading = ref(true)

onMounted(async () => {
  await handleShareLink()
})

async function handleShareLink() {
  const payload = route.params.payload
  await resolveSharePayload(payload.toString())
  loading.value = false
}
</script>
