<template>
  <main>
    <div div class="row">
      <div class="flex-shrink-0 p-3" style="width: 280px">
        <ul class="list-unstyled ps-0">
          <li class="mb-1">
            <button
              class="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed"
              data-bs-toggle="collapse"
              data-bs-target="#home-collapse"
              aria-expanded="true"
            >
              Home
            </button>
            <div class="collapse show" id="home-collapse">
              <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Overview</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Updates</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Reports</a
                  >
                </li>
              </ul>
            </div>
          </li>
          <li class="mb-1">
            <button
              class="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed"
              data-bs-toggle="collapse"
              data-bs-target="#dashboard-collapse"
              aria-expanded="false"
            >
              Dashboard
            </button>
            <div class="collapse" id="dashboard-collapse">
              <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Overview</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Weekly</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Monthly</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Annually</a
                  >
                </li>
              </ul>
            </div>
          </li>
          <li class="mb-1">
            <button
              class="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed"
              data-bs-toggle="collapse"
              data-bs-target="#orders-collapse"
              aria-expanded="false"
            >
              Orders
            </button>
            <div class="collapse" id="orders-collapse">
              <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >New</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Processed</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Shipped</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Returned</a
                  >
                </li>
              </ul>
            </div>
          </li>
          <li class="border-top my-3"></li>
          <li class="mb-1">
            <button
              class="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed"
              data-bs-toggle="collapse"
              data-bs-target="#account-collapse"
              aria-expanded="false"
            >
              Account
            </button>
            <div class="collapse" id="account-collapse">
              <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >New...</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Profile</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Settings</a
                  >
                </li>
                <li>
                  <a href="#" class="link-body-emphasis d-inline-flex text-decoration-none rounded"
                    >Sign out</a
                  >
                </li>
              </ul>
            </div>
          </li>
        </ul>
      </div>
    </div>

    <h1>Notes</h1>
  </main>
</template>

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
