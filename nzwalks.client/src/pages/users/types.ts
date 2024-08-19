import { Project } from '../projects/types'

export type UserRole = 'admin' | 'user' | 'owner'

export type User = {
  id: string
  fullname: string
  addressLine1: string
  addressLine2: string
  city: string
  postalCode: string
  country: string
  username: string
  email: string
  phoneNumber: string
  role: UserRole
  avatar: string
  projects: Project[]
  notes: string
  active: boolean
}
