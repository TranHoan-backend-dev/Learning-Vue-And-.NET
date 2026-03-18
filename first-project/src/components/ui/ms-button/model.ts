export interface ButtonProps {
    label?: string,
    className?: string,
    variant?: 'outlined' | 'tonal' | 'text' | 'plain'
    type?: 'button' | 'submit' | 'reset',
    density?: 'compact' | 'comfortable' | 'default',
    size?: 'x-small' | 'small' | 'large' | 'x-large',
    isLoading: boolean,
}