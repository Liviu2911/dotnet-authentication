type Props = {
  children: React.ReactNode
  submit: (e: React.FormEvent<HTMLFormElement>) => void
}

function Form({ children, submit }: Props) {
  return (
    <div className="absolute left-0 top-0 w-full h-[100vh] flex items-center justify-center">
      <form onSubmit={submit} className="flex flex-col items-center gap-4">
        {children}
      </form>
    </div>
  )
}

export default Form
