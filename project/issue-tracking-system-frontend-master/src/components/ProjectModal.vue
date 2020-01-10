<template>
  <div>
    <template>
      <b-modal id="modal-scoped" centered @show="resetModal" @hidden="resetModal">
        <template v-slot:modal-header="{ close }">
          <h5>Creat Project</h5>
        </template>

        <template v-slot:default="{ close }">
          <form ref="form" @submit.stop.prevent="handleSubmit">
            <b-form-group id="input-group-1" label-for="name-input">
              <b-form-input
                id="name-input"
                placeholder="Name"
                v-model="name"
                required
                :state="nameState"
              ></b-form-input>
            </b-form-group>
            <b-form-group id="input-group-2" label-for="textarea-description">
              <b-form-textarea
                id="textarea-description"
                class="mt-2"
                placeholder="Description..."
                v-model="description"
                rows="5"
                required
              ></b-form-textarea>
            </b-form-group>
          </form>
        </template>

        <template v-slot:modal-footer="{ cancel }">
          <!-- Emulate built in modal footer ok and cancel button actions -->
          <b-button size="sm" @click="cancel()">Cancel</b-button>
          <b-button size="sm" variant="primary" @click="handleOk">Create</b-button>
        </template>
      </b-modal>
    </template>
  </div>
</template>

<script>
export default {
  name: "projectModal",
  props: ["createProject"],
  Data() {
    return {
      name: "",
      description: "",
      nameState: null,
      names: [""]
    };
  },
  methods: {
    sendName() {
      this.$emit("inputName", this.name);
    },
    resetModal() {
      this.name = "";
      this.description = "";
      this.nameState = null;
    },
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.nameState = valid ? "valid" : "invalid";
      return valid;
    },
    handleOk(bvModalEvt) {
      // Prevent modal from closing
      bvModalEvt.preventDefault();
      // Trigger submit handler
      this.handleSubmit();
    },
    handleSubmit() {
      // Exit when the form isn't valid
      if (!this.checkFormValidity()) {
        return;
      }
      // Hide the modal manually
      this.$nextTick(() => {
        this.sendName();
        this.$bvModal.hide("modal-scoped");
      });
    }
  }
};
</script>