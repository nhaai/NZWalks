import { sleep } from '../../services/utils'
import { User } from './../../pages/users/types'

export let users = [] as User[]

// Simulate API calls

export type Pagination = {
  page: number
  perPage: number
  total: number
}

export type Sorting = {
  sortBy: keyof User | undefined
  sortingOrder: 'asc' | 'desc' | null
}

export type Filters = {
  isActive: boolean
  search: string
}

const getSortItem = (obj: any, sortBy: string) => {
  return obj[sortBy]
}

export const getUsers = async (filters: Partial<Filters & Pagination & Sorting>) => {
  await sleep(1000)
  const { search, sortBy, sortingOrder } = filters
  const response = await fetch('api/Users?pageNumber=' + filters.page + '&pageSize=' + filters.perPage)
  const metadata = JSON.parse((await response.headers.get('x-pagination')) || '{}')
  let filteredUsers = (await response.json()) as User[]

  if (search) {
    filteredUsers = filteredUsers.filter((user) => user.fullname.toLowerCase().includes(search.toLowerCase()))
  }

  if (sortBy && sortingOrder) {
    filteredUsers = filteredUsers.sort((a, b) => {
      const first = getSortItem(a, sortBy)
      const second = getSortItem(b, sortBy)
      if (first > second) {
        return sortingOrder === 'asc' ? 1 : -1
      }
      if (first < second) {
        return sortingOrder === 'asc' ? -1 : 1
      }
      return 0
    })
  }

  const { page = 1, perPage = 10 } = filters || {}
  return {
    data: (users = filteredUsers),
    pagination: {
      page,
      perPage,
      total: metadata?.TotalItemCount || filteredUsers.length,
    },
  }
}

export const addUser = async (user: User) => {
  await sleep(1000)
  const requestOptions = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(user),
  }
  const response = await fetch('api/Users', requestOptions)
  const newUser = (await response.json()) as User
  users.unshift(newUser)
}

export const updateUser = async (user: User) => {
  await sleep(1000)
  const requestOptions = {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(user),
  }
  const response = await fetch('api/Users/' + user.id, requestOptions)
  const existingUser = (await response.json()) as User
  const index = users.findIndex((x) => x.id === user.id)
  users[index] = existingUser
}

export const removeUser = async (user: User) => {
  await sleep(1000)
  await fetch('api/Users/' + user.id, { method: 'DELETE' })
  users.splice(
    users.findIndex((x) => x.id === user.id),
    1,
  )
}
