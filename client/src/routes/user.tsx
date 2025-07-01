import { useQuery } from '@tanstack/react-query'
import { createFileRoute, useNavigate } from '@tanstack/react-router'
import { useEffect } from 'react'

export const Route = createFileRoute('/user')({
  component: RouteComponent,
})

function RouteComponent() {
  const nav = useNavigate()

  useEffect(() => {
    const checkLogin = async () => {
      const accessToken = document.cookie.split('=')[0] === 'accesstoken'
      if (!accessToken) {
        // get new one
        const res = await fetch('http://localhost:5000/api/newtoken', {
          method: 'GET',
          credentials: 'include',
          headers: {
            'Content-Type': 'application/json',
          },
        })

        const json = await res.json()
        if (json.error === 'logout') {
          await fetch('http://localhost:5000/api/logout', {
            method: 'DELETE',
            credentials: 'include',
            headers: {
              'Content-Type': 'application/json',
            },
          })
          nav({
            to: '/login',
          })
          return
        }
        return
      }
    }
    checkLogin()
  }, [])

  const logout = async () => {
    const res = await fetch('http://localhost:5000/api/logout', {
      method: 'DELETE',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    const json = await res.json()

    if (!json.error) {
      nav({
        to: '/login',
      })
      return
    }

    console.log(json.error)
  }

  const { data: user, error } = useQuery({
    queryKey: ['user'],
    queryFn: async () => {
      const res = await fetch('http://localhost:5000/api/user', {
        method: 'GET',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${document.cookie.split('=')[1]}`,
        },
      })
      return (await res.json()).user
    },
  })

  if (error) {
    console.log(error)
    return <h1>error</h1>
  }

  if (user) {
    console.log(user)
    return (
      <>
        <button onClick={logout}>Log Out</button>
        <h1>Username: {user.username}</h1>
        <h1>Email: {user.email}</h1>
      </>
    )
  }

  return <h1>loading..</h1>
}
