<template>
  <main class="m-2">
    <div class="form-group">
      <input v-model="search" class="form-control" placeholder="Search..." />
    </div>

    <table class="table mt-4">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Filename</th>
          <th scope="col">Uploaded on</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <th scope="row">1</th>
          <td>Mark</td>
          <td>Otto</td>
        </tr>
        <tr>
          <th scope="row">2</th>
          <td>Jacob</td>
          <td>Thornton</td>
        </tr>
        <tr>
          <th scope="row">3</th>
          <td>Larry the Bird</td>
          <td>@twitter</td>
        </tr>
      </tbody>
    </table>
  </main>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const search = ref('')
</script>
