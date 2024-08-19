<script setup lang="ts">
import { PropType, computed, ref, watch } from 'vue'
import { useForm } from 'vuestic-ui'
import { User, UserRole } from '../types'
import UserAvatar from './UserAvatar.vue'
import { useProjects } from '../../projects/composables/useProjects'
import { validators } from '../../../services/utils'

const props = defineProps({
  user: {
    type: Object as PropType<User | null>,
    default: null,
  },
  saveButtonLabel: {
    type: String,
    default: 'Save',
  },
})

const defaultNewUser: User = {
  id: '',
  avatar: '',
  fullname: '',
  addressLine1: '',
  addressLine2: '',
  city: '',
  postalCode: '',
  country: '',
  role: 'user',
  userName: '',
  notes: '',
  email: '',
  phoneNumber: '',
  active: true,
}

const newUser = ref<User>({ ...defaultNewUser })

const isFormHasUnsavedChanges = computed(() => {
  return Object.keys(newUser.value).some((key) => {
    if (key === 'avatar' || key === 'projects') {
      return false
    }

    return newUser.value[key as keyof User] !== (props.user ?? defaultNewUser)?.[key as keyof User]
  })
})

defineExpose({
  isFormHasUnsavedChanges,
})

watch(
  () => props.user,
  () => {
    if (!props.user) {
      return
    }

    newUser.value = {
      ...props.user,
      avatar: props.user.avatar || '',
    }
  },
  { immediate: true },
)

const avatar = ref<File>()

const makeAvatarBlobUrl = (avatar: File) => {
  return 'https://randomuser.me/api/portraits/men/' + Math.floor(Math.random() * 100) + '.jpg';
  // return URL.createObjectURL(avatar)
}

watch(avatar, (newAvatar) => {
  newUser.value.avatar = newAvatar ? makeAvatarBlobUrl(newAvatar) : ''
})

const form = useForm('add-user-form')

const emit = defineEmits(['close', 'save'])

const onSave = () => {
  if (form.validate()) {
    emit('save', newUser.value)
  }
}

const roleSelectOptions: { text: Capitalize<UserRole>; value: UserRole }[] = [
  { text: 'Admin', value: 'admin' },
  { text: 'User', value: 'user' },
  { text: 'Owner', value: 'owner' },
]

const { projects } = useProjects({ pagination: ref({ page: 1, perPage: 9999, total: 10 }) })
</script>

<template>
  <VaForm v-slot="{ isValid }" ref="add-user-form" class="flex-col justify-start items-start gap-4 inline-flex w-full">
    <VaFileUpload
      v-model="avatar"
      type="single"
      hide-file-list
      class="self-stretch justify-start items-center gap-4 inline-flex"
    >
      <UserAvatar :user="newUser" size="large" />
      <VaButton preset="primary" size="small">Add image</VaButton>
      <VaButton
        v-if="avatar"
        preset="primary"
        color="danger"
        size="small"
        icon="delete"
        class="z-10"
        @click.stop="avatar = undefined"
      />
    </VaFileUpload>
    <div class="self-stretch flex-col justify-start items-start gap-4 flex">
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newUser.fullname"
          label="Full Name"
          class="w-full sm:w-1/2"
          :rules="[validators.required]"
          name="fullname"
        />
        <VaInput
          v-model="newUser.phoneNumber"
          label="Phone Number"
          class="w-full sm:w-1/2"
          name="phoneNumber"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newUser.addressLine1"
          label="Address Line 1"
          class="w-full sm:w-1/2"
          name="addressLine1"
        />
        <VaInput
          v-model="newUser.addressLine2"
          label="Address Line 2"
          class="w-full sm:w-1/2"
          name="addressLine2"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newUser.city"
          label="City"
          class="w-full sm:w-1/2"
          name="city"
        />
        <VaInput
          v-model="newUser.postalCode"
          label="Postal Code"
          class="w-full sm:w-1/2"
          name="postalCode"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-if="!newUser.id"
          v-model="newUser.userName"
          label="Email"
          class="w-full sm:w-1/2"
          :rules="[validators.required, validators.email]"
          name="userName"
        />
        <VaInput
          v-if="!newUser.id"
          v-model="newUser.password"
          label="Password"
          class="w-full sm:w-1/2"
          :rules="[validators.required, validators.password]"
          name="password" />
      </div>

      <div class="flex gap-4 w-full">
        <div class="w-1/2">
          <VaSelect
            v-model="newUser.role"
            label="Role"
            class="w-full"
            :options="roleSelectOptions"
            :rules="[validators.required]"
            name="role"
            value-by="value"
          />
        </div>

        <div class="flex items-center w-1/2 mt-4">
          <VaCheckbox v-model="newUser.active" label="Active" class="w-full" name="active" />
        </div>
      </div>

      <VaTextarea v-model="newUser.notes" label="Notes" class="w-full" name="notes" />
      <div class="flex gap-2 flex-col-reverse items-stretch justify-end w-full sm:flex-row sm:items-center">
        <VaButton preset="secondary" color="secondary" @click="$emit('close')">Cancel</VaButton>
        <VaButton :disabled="!isValid" @click="onSave">{{ saveButtonLabel }}</VaButton>
      </div>
    </div>
  </VaForm>
</template>
