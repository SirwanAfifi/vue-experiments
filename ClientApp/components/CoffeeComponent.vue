<template>
    <div class="container">
        <h3>Add a new Coffee</h3>
        <form v-on:submit.prevent="submitted">
            <div class="form-group">
                <label class="control-label">Coffee Name</label>
                <input required type="text" class="form-control" name="Name" v-model="model.name">
            </div>

            <div class="form-group">
                <label class="control-label">Coffee Type</label>
                <select class="form-control" name="CoffeeType" v-model="model.coffeeType">
                    <option v-for="coffeeType in coffeeTypes" :value="coffeeType.value" v-bind:key="coffeeType.value">{{ coffeeType.name }}</option>
                </select>
            </div>

            <div class="form-group">
                <label class="control-label">Coffee Image</label>
                <input required type="file" class="form-control" name="Image" ref="image">
            </div>

            <button class="btn btn-primary" type="submit">Save</button>
        </form>

        <hr>

        <div class="row">
            <div v-if="uploadDetails.queueProgress > 0">
                <table class="table">
                    <thead>
                    <tr>
                        <th width="15%">Event</th>
                        <th>Status</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td><strong>Elapsed time</strong></td>
                        <td nowrap>{{uploadDetails.uploadTimeElapsed | number}} second(s)</td>
                    </tr>
                    <tr>
                        <td><strong>Remaining time</strong></td>
                        <td nowrap>{{uploadDetails.uploadTimeRemaining | number}} second(s)</td>
                    </tr>
                    <tr>
                        <td><strong>Upload speed</strong></td>
                        <td nowrap>{{uploadDetails.uploadSpeed | number}} MB/s</td>
                    </tr>
                    <tr>
                        <td><strong>Queue progress</strong></td>
                        <td>
                        <div class="progress-bar progress-bar-info progress-bar-striped" role="progressbar"
                            aria-valuemin="0" aria-valuemax="100" :aria-valuenow="uploadDetails.queueProgress"
                            :style="{ 'width': uploadDetails.queueProgress + '%' }">
                            {{uploadDetails.queueProgress}}%
                        </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>
        </div>

    </div>
</template>

<script lang="ts">
import Vue from "vue";
import { eventBus } from "../main";
import { Component, Prop, Watch } from "vue-property-decorator";
import { coffeeService } from "../services/CoffeeService";
/// <reference path="./toaster.d.ts" />

@Component({
  components: { UploadFileSimpleComponent }
})
export default class UploadFileSimpleComponent extends Vue {
  coffeeTypes = [
    { name: "Espresso", value: 0 },
    { name: "Latte", value: 1 },
    { name: "Mocha", value: 2 }
  ];

  model = {};

  uploadDetails = {
    queueProgress: "",
    uploadTimeRemaining: "",
    uploadTimeElapsed: "",
    uploadSpeed: ""
  };

  submitted() {
    coffeeService
      .saveCoffee(this.model, (<any>this.$refs.image).files)
      .then(function(response: any) {
        toastr.success(response.data);
      })
      .catch(function(error: any) {
        toastr.error(error);
      });
  }
  
  created() {
    eventBus.$on("uploadData", (details: any) => {
      console.log(details);
      this.uploadDetails.queueProgress = details.queueProgress;
      this.uploadDetails.uploadTimeRemaining = details.uploadTimeRemaining;
      this.uploadDetails.uploadTimeElapsed = details.uploadTimeElapsed;
      this.uploadDetails.uploadSpeed = details.uploadSpeed;
    });
  }
}
</script>

<style scoped>
</style>
