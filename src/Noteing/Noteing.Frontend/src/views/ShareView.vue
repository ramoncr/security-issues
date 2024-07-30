<template>
    <div class="m-2">
        <h3>Share</h3>
       </div>

</template>

<script setup lang="ts">
import { resolveSharePayload } from '@/services/share.service';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';


const route = useRoute();


onMounted(async () => {
    const payload = route.params.payload;

    await resolveSharePayload(payload.toString());
});






</script>